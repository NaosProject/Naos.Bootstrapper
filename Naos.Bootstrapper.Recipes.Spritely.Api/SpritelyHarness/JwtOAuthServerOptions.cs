// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JwtOAuthServerOptions.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using System;
    using Microsoft.Owin.Security.OAuth;

    /// <summary>
    /// Provides a set of options for a JWT OAuth authorization server.
    /// </summary>
    /// <seealso cref="OAuthAuthorizationServerOptions"/>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Auth", Justification = "Spelling/name is correct.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Jwt", Justification = "Spelling/name is correct.")]
    public class JwtOAuthServerOptions : OAuthAuthorizationServerOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JwtOAuthServerOptions"/> class.
        /// </summary>
        /// <param name="serverSettings">The server settings.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="jwtAccessTokenFormat">The JWT format.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "jwt", Justification = "Spelling/name is correct.")]
        public JwtOAuthServerOptions(
            JwtOAuthServerSettings serverSettings,
            IOAuthAuthorizationServerProvider provider,
            JwtAccessTokenFormat jwtAccessTokenFormat)
        {
            if (serverSettings == null)
            {
                throw new ArgumentNullException(nameof(serverSettings));
            }

            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            if (jwtAccessTokenFormat == null)
            {
                throw new ArgumentNullException(nameof(jwtAccessTokenFormat));
            }

            this.AuthenticationType = "JWT";
            this.AllowInsecureHttp = serverSettings.AllowInsecureHttp;
            this.AccessTokenExpireTimeSpan = serverSettings.AccessTokenExpireTimeSpan;
            this.TokenEndpointPath = serverSettings.TokenEndpointPath;

            this.Provider = provider;
            this.AccessTokenFormat = jwtAccessTokenFormat;
        }
    }
}
