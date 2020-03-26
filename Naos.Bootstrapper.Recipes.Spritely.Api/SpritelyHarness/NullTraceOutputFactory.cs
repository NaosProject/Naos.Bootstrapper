// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NullTraceOutputFactory.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using System.IO;
    using Microsoft.Owin.Hosting.Tracing;

    /// <summary>
    /// A default no-op implementation of the trace output factory.
    /// </summary>
    /// <seealso cref="Microsoft.Owin.Hosting.Tracing.ITraceOutputFactory" />
    public class NullTraceOutputFactory : ITraceOutputFactory
    {
        /// <summary>
        /// Used to create the trace output.
        /// </summary>
        /// <param name="outputFile">Ignored. Here to satisfy interface.</param>
        /// <returns>A null StreamWriter.</returns>
        public TextWriter Create(string outputFile)
        {
            return StreamWriter.Null;
        }
    }
}