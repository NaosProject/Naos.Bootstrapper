// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StoreByThumbprintCertificateFetcher.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using System;
    using System.Globalization;
    using System.Security.Cryptography.X509Certificates;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Fetches certificates from the X509 certificate store by thumbprint.
    /// </summary>
    /// <seealso cref="ICertificateFetcher"/>
    public class StoreByThumbprintCertificateFetcher : ICertificateFetcher
    {
        private readonly StoreCertificate storeCertificate;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreByThumbprintCertificateFetcher"/> class.
        /// </summary>
        /// <param name="storeCertificate">The store certificate.</param>
        /// <exception cref="System.ArgumentNullException">If any arguments are null.</exception>
        public StoreByThumbprintCertificateFetcher(StoreCertificate storeCertificate)
        {
            this.storeCertificate = storeCertificate ?? throw new ArgumentNullException(nameof(storeCertificate));
        }

        /// <summary>
        /// Fetches a certificate.
        /// </summary>
        /// <returns>The certificate.</returns>
        public X509Certificate2 Fetch()
        {
            var certificateStore = new X509Store(this.storeCertificate.StoreName, this.storeCertificate.StoreLocation);

            try
            {
                certificateStore.Open(OpenFlags.OpenExistingOnly);

                var thumbprint =
                    Regex.Replace(this.storeCertificate.CertificateThumbprint, @"[^\da-zA-z]", string.Empty)
                        .ToUpper(CultureInfo.InvariantCulture);

                var certificates = certificateStore.Certificates.Find(
                    X509FindType.FindByThumbprint,
                    thumbprint,
                    this.storeCertificate.CertificateValidityRequired);

                var certificate = certificates.Count > 0 ? certificates[0] : null;

                return certificate;
            }
            finally
            {
                certificateStore.Close();
            }
        }
    }
}
