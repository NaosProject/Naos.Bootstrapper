﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleAbstraction.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in 'Naos.Bootstrapper.Recipes.Hangfire.Console' source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

#if NaosBootstrapper
namespace Naos.Bootstrapper.Recipes.Spritely.Api
#else
namespace $rootnamespace$
#endif
{
    using System;
    using System.Globalization;
    using System.Linq;

    using CLAP;
    using Naos.Bootstrapper;

    using Its.Log.Instrumentation;

    using Microsoft.Owin.Hosting;
    using Microsoft.Owin.Hosting.Tracing;

    using Naos.Configuration.Domain;

    using OBeautifulCode.Serialization;
    using OBeautifulCode.Serialization.Json;

    /// <summary>
    /// Abstraction for use with <see cref="CLAP" /> to provide basic command line interaction.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1053:StaticHolderTypesShouldNotHaveConstructors", Justification = "Cannot be static for command line contract.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Hangfire", Justification = "Spelling/name is correct.")]
    public partial class ConsoleAbstraction : ConsoleAbstractionBase
    {
        /// <summary>
        /// Listen for web service requests.
        /// </summary>
        /// <param name="debug">Optional indication to launch the debugger from inside the application (default is false).</param>
        /// <param name="environment">Optional value to use when setting the Naos.Configuration precedence to use specific settings.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Catching and swallowing on purpose.")]
        [Verb(Aliases = nameof(WellKnownConsoleVerb.Listen), IsDefault = false, Description = "Listen for web service requests.;\r\n            example usage: [Harness].exe listen\r\n                           [Harness].exe listen /debug=true\r\n                           [Harness].exe listen /environment=ExampleDevelopment\r\n                           [Harness].exe listen /environment=ExampleDevelopment /debug=true\r\n")]
        public static void Listen(
            [Aliases("")] [Description("Launches the debugger.")] [DefaultValue(false)] bool debug,
            [Aliases("")] [Description("Sets the Naos.Configuration precedence to use specific settings.")] [DefaultValue(null)] string environment)
        {
            /*---------------------------------------------------------------------------*
             * Any method should run this logic to debug, setup config & logging, etc.   *
             *---------------------------------------------------------------------------*/
            CommonSetup(debug, environment);

            /*---------------------------------------------------------------------------*
             * Any method should run this logic to write telemetry info to the log.      *
             *---------------------------------------------------------------------------*/
            WriteStandardTelemetry();

            /*---------------------------------------------------------------------------*
             * Launch the harness here.                                                  *
             *---------------------------------------------------------------------------*/
            try
            {
                var configuration = new StartupConfiguration();
                Config.Deserialize = configuration.DeserializeConfigurationSettings;
                var hostingSettings = Config.Get<HostingSettings>();
                var startOptions = new StartOptions(hostingSettings.Urls.First());
                hostingSettings.Urls.Skip(1).ToList().ForEach(startOptions.Urls.Add);

                // Disable built-in owin tracing by using a null trace output
                // see: https://stackoverflow.com/questions/37527531/owin-testserver-logs-multiple-times-while-testing-how-can-i-fix-this/37548074#37548074
                // and: http://stackoverflow.com/questions/17948363/tracelistener-in-owin-self-hosting
                startOptions.Settings.Add(
                    typeof(ITraceOutputFactory).FullName ?? throw new NotSupportedException(FormattableString.Invariant($"The type '{nameof(ITraceOutputFactory)}' should have a property '{nameof(Type.FullName)}'.")),
                    typeof(NullTraceOutputFactory).AssemblyQualifiedName);

                using (WebApp.Start<Startup>(startOptions))
                {
                    Log.Write(string.Format(CultureInfo.InvariantCulture, Messages.Web_Server_Running, string.Join(", ", hostingSettings.Urls)));
                    System.Console.WriteLine(Messages.Quit);
                    System.Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                System.Console.Write(ex);
            }

            Log.Write(string.Format(CultureInfo.InvariantCulture, Messages.Web_Server_Terminated));
        }
    }
}
