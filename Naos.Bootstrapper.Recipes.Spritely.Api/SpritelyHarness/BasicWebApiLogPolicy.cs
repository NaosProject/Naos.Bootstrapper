// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicWebApiLogPolicy.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Web.Http.ExceptionHandling;
    using Its.Log.Instrumentation;
    using Microsoft.Owin;

    /// <summary>
    /// Container for static log policy initialization logic.
    /// </summary>
    public static class BasicWebApiLogPolicy
    {
        private static int isSubscribed = 0;
        private static EventHandler<InstrumentationEventArgs> logSubscription = (sender, args) =>
        {
            var subject = args.LogEntry.Subject ?? string.Empty;
            var message = DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture) + ": " +
                          subject.ToLogString();

            Log(message);
        };

        /// <summary>
        /// Gets or sets the method called to perform actual logging. Defaults to writing to Trace Listeners.
        /// </summary>
        public static WriteLog Log { get; set; } = s => Trace.WriteLine(s);

        /// <summary>
        /// Initializes the log policy to write output to Tracing and registers WebApi's
        /// ExceptionLoggerContext object for additional output.
        /// </summary>
        public static void Initialize()
        {
            Formatter<OwinRequest>.RegisterForMembers(
                o => o.Method,
                o => o.Uri,
                o => o.Headers);

            Formatter<OwinResponse>.RegisterForMembers(
                o => o.StatusCode,
                o => o.ReasonPhrase,
                o => o.Headers);

            Formatter<HeaderDictionary>.Register(d => string.Join("; ", d.Keys.Select(k => k + ": " + string.Join(", ", d.GetValues(k)))));

            Formatter<ExceptionLoggerContext>.RegisterForAllMembers();

            // Reentrancy allowed but expectation it is still that users will call only once at startup
            if (Interlocked.CompareExchange(ref isSubscribed, 1, 0) == 0)
            {
                Its.Log.Instrumentation.Log.EntryPosted += logSubscription;
            }
        }
    }
}
