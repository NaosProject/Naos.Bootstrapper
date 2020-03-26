// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JwtOAuthServerSettings.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Owin;

    /// <summary>
    /// Provides settings for an JWT OAuth authorization server.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Auth", Justification = "Spelling/name is correct.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Jwt", Justification = "Spelling/name is correct.")]
    public class JwtOAuthServerSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether insecure HTTP connections are allowed. True to
        /// allow authorize and token requests to arrive on HTTP URI addresses, and to allow incoming
        /// redirect_uri authorize request parameter to have HTTP URI addresses.
        /// </summary>
        /// <value><c>true</c> if insecure HTTP connections are allowed; otherwise, <c>false</c>.</value>
        public bool AllowInsecureHttp { get; set; } = false;

        /// <summary>
        /// Gets or sets the token endpoint path. The request path client applications communicate
        /// with directly as part of the OAuth protocol. Must begin with a leading slash, like
        /// "/Token". If the client is issued a client_secret, it must be provided to this endpoint.
        /// </summary>
        /// <value>The token endpoint path.</value>
        public PathString TokenEndpointPath { get; set; } = new PathString("/token");

        /// <summary>
        /// Gets or sets the access token expire time span. The period of time the access token
        /// remains valid after being issued. The client application is expected to refresh or
        /// acquire a new access token after the token has expired.
        /// </summary>
        /// <value>The access token expire time span.</value>
        public TimeSpan AccessTokenExpireTimeSpan { get; set; } = TimeSpan.FromDays(1);

        /// <summary>
        /// Gets or sets the issuer. This is normally the URI where this service is hosted.
        /// </summary>
        /// <value>The issuer.</value>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets the allowed clients.
        /// </summary>
        /// <value>The allowed clients.</value>
        public ICollection<JwtOAuthClient> AllowedClients { get; } = new List<JwtOAuthClient>();
    }
}
