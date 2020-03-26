// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JwtOAuthClient.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    /// <summary>
    /// Describes a JWT OAuth client.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Auth", Justification = "Spelling/name is correct.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Jwt", Justification = "Spelling/name is correct.")]
    public class JwtOAuthClient
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the base 64 url encoded secret shared with this client.
        /// </summary>
        /// <value>The secret.</value>
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the relative file certificate.
        /// </summary>
        /// <value>
        /// The relative file certificate.
        /// </value>
        public RelativeFileCertificate RelativeFileCertificate { get; set; }

        /// <summary>
        /// Gets or sets the store certificate.
        /// </summary>
        /// <value>
        /// The store certificate.
        /// </value>
        public StoreCertificate StoreCertificate { get; set; }
    }
}
