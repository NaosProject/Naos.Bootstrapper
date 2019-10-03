// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestRunner.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Naos.Bootstrapper.Test
{
    using System;
    using System.Threading;
    using OBeautifulCode.Reflection.Recipes;
    using OBeautifulCode.Type.Recipes;
    using OBeautifulCode.Validation.Recipes;
    using Xunit.Runners;
    using Xunit.Sdk;

    using static System.FormattableString;

    /// <summary>
    /// Runner for xUnit tests.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "For this item the implementation is as intended.")]
    public class TestRunner : IDisposable
    {
        // We use consoleLock because messages can arrive in parallel, so we want to make sure we get
        // consistent console output.
        private readonly object announcementLock = new object();

        private readonly Action<string> announcer;

        // Use an event to know when we're done.
        private readonly ManualResetEvent finished = new ManualResetEvent(false);

        // Start out assuming success; will be updated in failure event method.
        private bool seenFailures = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRunner" /> class.
        /// </summary>
        /// <param name="announcer">The announcer to get details about any failures.</param>
        public TestRunner(
            Action<string> announcer)
        {
            this.announcer = announcer;
        }

        /// <summary>
        /// Runs the type of all tests found (and not skipped) in the provided type.
        /// </summary>
        /// <param name="typeToRunTestsFrom">The type to run tests from.</param>
        /// <exception cref="TestClassException">If any failures are seen <see cref="TestClassException" /> will be thrown.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "ToRun", Justification = "Spelling/name is correct.")]
        public void RunAllTestsInType(Type typeToRunTestsFrom)
        {
            new { typeToRunTestsFrom }.Must().NotBeNull();
            var typeNameToRunTestsFrom = typeToRunTestsFrom.FullName;

            var testAssemblyPath = typeToRunTestsFrom.Assembly.GetCodeBaseAsPathInsteadOfUri();

            using (var runner = AssemblyRunner.WithAppDomain(testAssemblyPath))
            {
                runner.OnDiscoveryComplete = this.OnDiscoveryComplete;
                runner.OnTestOutput = this.OnTestOutput;
                runner.OnTestPassed = this.OnTestPassed;
                runner.OnTestFailed = this.OnTestFailed;
                runner.OnTestSkipped = this.OnTestSkipped;
                runner.OnExecutionComplete = this.OnExecutionComplete;

                lock (this.announcementLock)
                {
                    this.announcer(" * Discovering tests in type.");
                }

                runner.Start(typeNameToRunTestsFrom);

                this.finished.WaitOne();
                this.finished.Dispose();

                if (this.seenFailures)
                {
                    throw new TestClassException(Invariant($" * Type: {typeToRunTestsFrom.ToStringReadable()} had test failures; see output provided to {nameof(this.announcer)} in constructor."));
                }
            }
        }

        private void OnDiscoveryComplete(DiscoveryCompleteInfo info)
        {
            lock (this.announcementLock)
            {
                this.announcer(Invariant($" * Running {info.TestCasesToRun} of {info.TestCasesDiscovered} tests."));
            }
        }

        private void OnTestOutput(TestOutputInfo info)
        {
            lock (this.announcementLock)
            {
                this.announcer(Invariant($" - {info.TestDisplayName}: {info.Output}"));
            }
        }

        private void OnTestPassed(TestPassedInfo info)
        {
            lock (this.announcementLock)
            {
                this.announcer(Invariant($" ^ [PASS] {info.TestDisplayName}."));
            }
        }

        private void OnTestFailed(TestFailedInfo info)
        {
            lock (this.announcementLock)
            {
                this.announcer(Invariant($" ! [FAIL] {info.TestDisplayName}: {info.ExceptionMessage}."));
                if (info.ExceptionStackTrace != null)
                {
                    this.announcer(info.ExceptionStackTrace);
                }
            }

            this.seenFailures = true;
        }

        private void OnTestSkipped(TestSkippedInfo info)
        {
            lock (this.announcementLock)
            {
                this.announcer(Invariant($" # [SKIP] {info.TestDisplayName}: {info.SkipReason}"));
            }
        }

        private void OnExecutionComplete(ExecutionCompleteInfo info)
        {
            lock (this.announcementLock)
            {
                this.announcer(Invariant($" * Finished: {info.TotalTests} tests in {Math.Round(info.ExecutionTime, 3)}s ({info.TestsFailed} failed, {info.TestsSkipped} skipped)."));
            }

            this.finished.Set();
        }

        /// <inheritdoc />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly", Justification = "For this item the implementation is as intended.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "For this item the implementation is as intended.")]
        public void Dispose()
        {
            this.finished.Dispose();
        }
    }
}
