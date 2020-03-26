// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HostingSettings.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using System.Collections.Generic;

    /// <summary>
    /// Object representing the hosting settings.
    /// </summary>
    public class HostingSettings
    {
        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public ICollection<string> Urls { get; } = new List<string>();

        /// <summary>
        /// Gets or sets the CORS settings instance.
        /// </summary>
        /// <value>
        /// The CORS settings instance.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cors", Justification = "Spelling/name is correct.")]
        public Cors Cors { get; set; }
    }
}
