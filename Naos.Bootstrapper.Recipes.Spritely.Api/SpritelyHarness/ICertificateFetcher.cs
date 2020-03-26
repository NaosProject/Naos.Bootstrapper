// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICertificateFetcher.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// Represents an object capable of fetching certificates.
    /// </summary>
    public interface ICertificateFetcher
    {
        /// <summary>
        /// Fetches a certificate.
        /// </summary>
        /// <returns>The certificate.</returns>
        X509Certificate2 Fetch();
    }
}
