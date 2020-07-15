﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in 'Naos.Bootstrapper.Recipes.Core.Console' source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

#if NaosBootstrapper
namespace Naos.Bootstrapper
#else
namespace $rootnamespace$
#endif
{
    using System;
    using CLAP;
    using Its.Log.Instrumentation;
    using Naos.Bootstrapper;

    /// <summary>
    /// Main entry point of application.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("Naos.Bootstrapper.Recipes.Console", "See package version number")]
    public static class Program
    {
        /// <summary>
        /// Main entry point.
        /// </summary>
        /// <param name="args">Arguments for application.</param>
        /// <returns>Exit code.</returns>
        public static int Main(
            string[] args)
        {
            try
            {
                WriteAsciiArt(Console.WriteLine);

                var consoleAbstraction = new ConsoleAbstraction();

                // ConsoleAbstraction must derive from ConsoleAbstractionBase which is provided in the Bootstrapper recipes, it contains the implementation of this method.
                consoleAbstraction.PerformEntryPointPreChecks();

                // Need to register the exception types to the static context.
                ConsoleAbstractionBase.UpdateTypeRepresentationsOfExceptionsToOmitStackTraceFrom(consoleAbstraction.ExceptionTypeRepresentationsToOnlyPrintMessage);

                /*---------------------------------------------------------------------------*
                 * This is just a pass through to the CLAP implementation of the harness,    *
                 * it will parse the command line arguments and provide multiple entry       *
                 * points as configured.  It is easiest to derive from the abstract class    *
                 * 'ConsoleAbstractionBase' as 'ExampleConsoleAbstraction' does which        *
                 * provides an example of the minimum amount of work to get started.  It is  *
                 * installed as a recipe for easy reference and covers help, errors, etc.    *
                 *---------------------------------------------------------------------------*
                 * For an example of config files you can install the package                *
                 * 'Naos.Recipes.Console.ExampleConfig' which has examples of the directory  *
                 * structure, 'LogProcessorSettings' settings for console and file, as well  *
                 * as an App.Config it not using the environment name as a parameter.        *
                 *---------------------------------------------------------------------------*
                 * Must update the code below to use your custom abstraction class.          *
                 *---------------------------------------------------------------------------*/
                var exitCode = Parser.Run<ConsoleAbstraction>(args);
                return exitCode;
            }
            catch (Exception ex)
            {
                /*---------------------------------------------------------------------------*
                 * This should never be reached but is here as a last ditch effort to ensure *
                 * errors are not lost.                                                      *
                 *---------------------------------------------------------------------------*/
                Console.WriteLine(string.Empty);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(string.Empty);
                Log.Write(ex);

                return 1;
            }
        }

        private static void WriteAsciiArt(
            Action<string> announcer)
        {
            /*---------------------------------------------------------------------------*
             * Totally unecessary but just plain fun, link to build your own.            *
             * http://patorjk.com/software/taag/#f=Small%20Keyboard&t=NAOS%20Console     *
             * https://manytools.org/hacker-tools/convert-images-to-ascii-art/           *
             *---------------------------------------------------------------------------*/

            announcer(string.Empty);
            announcer(@"|_______________________________________________________________________|");
            announcer(@"|                                                                       |");
            announcer(@"|                                                                       |");
            announcer(@"|                                                                       |");
            announcer(@"|      %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%      |");
            announcer(@"|      %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%      |");
            announcer(@"|      %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%      |");
            announcer(@"|      %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%      |");
            announcer(@"|      %%%%%,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,%%%%%      |");
            announcer(@"|      %%%%%,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,%%%%%      |");
            announcer(@"|      %%%%%,,,,%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%,,,,%%%%%      |");
            announcer(@"|      %%%%%,,,,%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%,,,,%%%%%      |");
            announcer(@"|      %%%%%,,,,%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%,,,,%%%%%      |");
            announcer(@"|      %%%%%,,,,%%%%%%%%%%%%%%/,,,,,,,,,,,%%%%%%%%%%%%%%%,,,,%%%%%      |");
            announcer(@"|      %%%%%,,,,%%%%%%%%%%%,,,,,,,,,,,,,,,,,,%%%%%%%%%%%%,,,,%%%%%      |");
            announcer(@"|      %%%%%%%%%%%%%%%%%%,,,,,,%%%%%%%%%%*,,,,,%%%%%%%%%%,,,,%%%%%      |");
            announcer(@"|      %%%%%%%%%%%%%%%%%,,,,,%%%%%%%%%%%%%%%,,,,,%%%%%%%%,,,,%%%%%      |");
            announcer(@"|      %%%%%%%%%%%%%%%%,,,,%%%%%%%%%%%%%%%%%%,,,,#%%%%%%%,,,,%%%%%      |");
            announcer(@"|      %%%%%%%%%%%%%%%(,%,#%%%%%%%%%%%%%%%%%%%,,,,%%%%%%%,,,,%%%%%      |");
            announcer(@"|      %%%%%%%%%%%%%%%,%%,%%%%%%%%%%%%%%%%%%%%*,,,%%%%%%%,,,,%%%%%      |");
            announcer(@"|      %%%%%%%%%%%%%%%,%%,%%%%%%%%%%%%%%%%%%%%,,,,%%%%%%%,,,,%%%%%      |");
            announcer(@"|      %%%%%%%%%%%%%%%%,%,*%%%%%%%%%%%%%%%%%%%,,,,%%%%%%%,,,,%%%%%      |");
            announcer(@"|      %%%%%%%%%%%%%%%%,,,,/%%%%%%%%%%%%%%%%%,,,,%%%%%%%%,,,,%%%%%      |");
            announcer(@"|      %%%%%%%%%%%%%%%%%,,,,,%%%%%%%%%%%%%%*,,,,%%%%%%%%%,,,,%%%%%      |");
            announcer(@"|      %%%%%%%%%%%%%%%%%%%,,,,,,%%%%%%%%%,,,,,,%%%%%%%%%%,,,,%%%%%      |");
            announcer(@"|      %%%%%,,,,%%%%%%%%%%%%,,,,,,,,,,,,,,,,,%%%%%%%%%%%%,,,,%%%%%      |");
            announcer(@"|      %%%%%,,,,%%%%%%%%%%%%%%%/,,,,,,,,,%%%%%%%%%%%%%%%%,,,,%%%%%      |");
            announcer(@"|      %%%%%,,,,%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%,,,,%%%%%      |");
            announcer(@"|      %%%%%,,,,%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%,,,,%%%%%      |");
            announcer(@"|      %%%%%,,,,%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%,,,,%%%%%      |");
            announcer(@"|      %%%%%,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,%%%%%      |");
            announcer(@"|      %%%%%,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,%%%%%      |");
            announcer(@"|      %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%      |");
            announcer(@"|      %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%      |");
            announcer(@"|      %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%      |");
            announcer(@"|                                                                       |");
            announcer(@"|                                                                       |");
            announcer(@"|_______________________________________________________________________|");
            announcer(string.Empty);
        }
    }
}