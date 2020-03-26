// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JwtAccessTokenFormat.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using System;
    using System.Globalization;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Security.Cryptography;
    using Jose;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.DataHandler.Encoder;

    /// <summary>
    /// Formats an authentication ticket as a JSON Web token.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Jwt", Justification = "Spelling/name is correct.")]
    public class JwtAccessTokenFormat : ISecureDataFormat<AuthenticationTicket>
    {
        /// <summary>
        /// The hmac sha512 signature.
        /// </summary>
        public const string HmacSha512Signature = "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512";

        /// <summary>
        /// The sha512 digest.
        /// </summary>
        public const string Sha512Digest = "http://www.w3.org/2001/04/xmlenc#sha512";

        private readonly JwtOAuthServerSettings serverSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtAccessTokenFormat" /> class.
        /// </summary>
        /// <param name="serverSettings">The server settings.</param>
        /// <exception cref="System.ArgumentNullException">If serverSettings is null.</exception>
        /// <exception cref="System.InvalidOperationException">If serverSettings configuration is invalid.</exception>
        /// <exception cref="System.FormatException">If a client secret is not base 64 url encoded.</exception>
        public JwtAccessTokenFormat(JwtOAuthServerSettings serverSettings)
        {
            if (serverSettings == null)
            {
                throw new ArgumentNullException(nameof(serverSettings));
            }

            this.serverSettings = serverSettings;

            foreach (var client in this.serverSettings.AllowedClients)
            {
                if (client.RelativeFileCertificate != null && client.StoreCertificate != null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, Messages.Exception_JwtAccessTokenFormat_MultipleCertificateOptionsProvided, client.Id));
                }

                var certificateFetcher = GetCertificateFetcher(client);

                if (certificateFetcher != null)
                {
                    var certificate = certificateFetcher.Fetch();

                    if (certificate == null)
                    {
                        throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, Messages.Exception_JwtAccessTokenFormat_CertificateNotFound, client.Id));
                    }
                }

                // Try decoding each secret early to detect if there is a configuration problem
                TextEncodings.Base64Url.Decode(client.Secret);
            }
        }

        /// <summary>
        /// Protects the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>A JWT token.</returns>
        /// <exception cref="ArgumentNullException">If data is null.</exception>
        /// <exception cref="InvalidOperationException">
        /// If AuthenticationTicket.Properties does not include audience.
        /// </exception>
        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var clientId = data.Properties.Dictionary.ContainsKey("audience")
                ? data.Properties.Dictionary["audience"]
                : null;

            if (string.IsNullOrWhiteSpace(clientId))
            {
                throw new InvalidOperationException(Messages.Exception_JwtAccessTokenFormat_NoAudience);
            }

            var client = this.serverSettings.AllowedClients.FirstOrDefault(c => c.Id == clientId);
            if (client == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, Messages.Exception_JwtAccessTokenFormat_InvalidClientId, clientId));
            }

            var jwe = this.GetJwe(data, client, clientId);

            return jwe;
        }

        private string GetJwe(AuthenticationTicket data, JwtOAuthClient client, string clientId)
        {
            var certificateFetcher = GetCertificateFetcher(client);
            var securityKey = TextEncodings.Base64Url.Decode(client.Secret);
            var jwt = this.GetJwt(data, securityKey, clientId);

            var publicKey = certificateFetcher?.Fetch()?.PublicKey.Key as RSACryptoServiceProvider;

            var jwe = publicKey != null
                ? JWT.Encode(jwt, publicKey, JweAlgorithm.RSA_OAEP_256, JweEncryption.A256GCM, JweCompression.DEF)
                : jwt;

            return jwe;
        }

        private static ICertificateFetcher GetCertificateFetcher(JwtOAuthClient client)
        {
            var certificateFetcher =
                client.RelativeFileCertificate != null
                    ? new FileCertificateFetcher(client.RelativeFileCertificate)
                    : client.StoreCertificate != null
                        ? new StoreByThumbprintCertificateFetcher(client.StoreCertificate)
                        : null as ICertificateFetcher;

            return certificateFetcher;
        }

        private string GetJwt(AuthenticationTicket data, byte[] securityKey, string clientId)
        {
            var issued = data.Properties.IssuedUtc?.UtcDateTime;
            var expires = data.Properties.ExpiresUtc?.UtcDateTime;

            var signingCredentials = new SigningCredentials(
                new InMemorySymmetricSecurityKey(securityKey),
                HmacSha512Signature,
                Sha512Digest);

            var token = new JwtSecurityToken(
                this.serverSettings.Issuer ?? string.Empty,
                clientId,
                data.Identity.Claims,
                issued,
                expires,
                signingCredentials);

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.WriteToken(token);

            return jwt;
        }

        /// <summary>
        /// Unprotects the specified protected text.
        /// </summary>
        /// <param name="protectedText">The protected text.</param>
        /// <returns>Nothing - always throws.</returns>
        /// <exception cref="System.NotImplementedException">Always thrown.</exception>
        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}
