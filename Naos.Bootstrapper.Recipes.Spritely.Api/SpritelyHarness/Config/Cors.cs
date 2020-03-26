// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Cors.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using System.Collections.Generic;

    /// <summary>
    ///     Object representing CORS settings.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cors", Justification = "Spelling/name is correct.")]
    public class Cors
    {
        /// <summary>
        /// Gets or sets a value indicating whether credentials are supported.
        /// </summary>
        /// <value>
        ///   <c>true</c> if credentials are supported; otherwise, <c>false</c>.
        /// </value>
        public bool SupportsCredentials { get; set; }

        /// <summary>
        /// Gets or sets the preflight maximum age.
        /// </summary>
        /// <value>
        /// The preflight maximum age.
        /// </value>
        public long? PreflightMaxAge { get; set; }

        /// <summary>
        /// Gets the origins.
        /// </summary>
        /// <value>
        /// The origins.
        /// </value>
        public ICollection<string> Origins { get; } = new List<string>();

        /// <summary>
        /// Gets the headers.
        /// </summary>
        /// <value>
        /// The headers.
        /// </value>
        public ICollection<string> Headers { get; } = new List<string>();

        /// <summary>
        /// Gets the methods.
        /// </summary>
        /// <value>
        /// The methods.
        /// </value>
        public ICollection<string> Methods { get; } = new List<string>();

        /// <summary>
        /// Gets the exposed headers.
        /// </summary>
        /// <value>
        /// The exposed headers.
        /// </value>
        public ICollection<string> ExposedHeaders { get; } = new List<string>();
    }
}
