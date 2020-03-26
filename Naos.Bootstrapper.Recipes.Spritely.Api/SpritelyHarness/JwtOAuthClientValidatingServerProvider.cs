// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JwtOAuthClientValidatingServerProvider.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Its.Log.Instrumentation;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;

    /// <summary>
    /// Provides an authorization implementation for a Jwt OAuth server that validates clients based
    /// on the given OAuth server settings.
    /// </summary>
    /// <seealso cref="OAuthAuthorizationServerProvider"/>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Auth", Justification = "Spelling/name is correct.")]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Jwt", Justification = "Spelling/name is correct.")]
    public class JwtOAuthClientValidatingServerProvider : OAuthAuthorizationServerProvider
    {
        /// <summary>
        /// Gets the server settings.
        /// </summary>
        /// <value>The server settings.</value>
        protected JwtOAuthServerSettings ServerSettings { get; }

        /// <summary>
        /// Gets the authenticator.
        /// </summary>
        /// <value>The authenticator.</value>
        protected IAuthenticator Authenticator { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtOAuthClientValidatingServerProvider"/> class.
        /// </summary>
        /// <param name="serverSettings">The server settings.</param>
        /// <param name="authenticator">The authenticator.</param>
        /// <exception cref="System.ArgumentNullException">If any arguments are null.</exception>
        public JwtOAuthClientValidatingServerProvider(
            JwtOAuthServerSettings serverSettings,
            IAuthenticator authenticator)
        {
            if (serverSettings == null)
            {
                throw new ArgumentNullException(nameof(serverSettings));
            }

            if (authenticator == null)
            {
                throw new ArgumentNullException(nameof(authenticator));
            }

            this.ServerSettings = serverSettings;
            this.Authenticator = authenticator;
        }

        /// <summary>
        /// Called to validate that the origin of the request is a registered "client_id", and that
        /// the correct credentials for that client are present on the request. If the web
        /// application accepts Basic authentication credentials, context.TryGetBasicCredentials(out
        /// clientId, out clientSecret) may be called to acquire those values if present in the
        /// request header. If the web application accepts "client_id" and "client_secret" as form
        /// encoded POST parameters, context.TryGetFormCredentials(out clientId, out clientSecret)
        /// may be called to acquire those values if present in the request body. If
        /// context.Validated is not called the request will not proceed further.
        /// </summary>
        /// <param name="context">The context of the event carries information in and results out.</param>
        /// <returns>Task to enable asynchronous execution.</returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // This code may appear to not be useful, but a side-effect of calling the Try methods is
            // to set context.clientId
            string clientId;
            string clientSecret;
            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            // ReSharper disable once SimplifyLinqExpression
            if (!this.ServerSettings.AllowedClients.Any(c => c.Id == context.ClientId))
            {
                context.SetError(
                    "invalid_client_id",
                    string.Format(CultureInfo.InvariantCulture, Messages.OAuthError_InvalidClientId, context.ClientId));
                return Task.FromResult<object>(null);
            }

            context.Validated();
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Called when a request to the Token endpoint arrives with a "grant_type" of "password".
        /// This occurs when the user has provided name and password credentials directly into the
        /// client application's user interface, and the client application is using those to acquire
        /// an "access_token" and optional "refresh_token". If the web application supports the
        /// resource owner credentials grant type it must validate the context.Username and
        /// context.Password as appropriate. To issue an access token the context.Validated must be
        /// called with a new ticket containing the claims about the resource owner which should be
        /// associated with the access token. The application should take appropriate measures to
        /// ensure that the endpoint isn’t abused by malicious callers. The default behavior is to
        /// reject this grant type. See also http://tools.ietf.org/html/rfc6749#section-4.3.2.
        /// </summary>
        /// <param name="context">The context of the event carries information in and results out.</param>
        /// <returns>Task to enable asynchronous execution.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "We do not know what exception types a user may throw so we scope the handling as tight as possible and catch everything.")]
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var credentials = new Credentials
            {
                AuthenticationType = context.Options.AuthenticationType,
                UserName = context.UserName,
                Password = context.Password,
            };

            ClaimsIdentity identity;
            try
            {
                identity = this.Authenticator.SignIn(credentials);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                context.SetError("invalid_grant", ex.Message);
                return Task.FromResult<object>(null);
            }

            if (identity == null)
            {
                Log.Write(Messages.OAuthError_InvalidGrant);
                context.SetError("invalid_grant", Messages.OAuthError_InvalidGrant);
                return Task.FromResult<object>(null);
            }

            var properties = new AuthenticationProperties(
                new Dictionary<string, string>
                {
                    {
                        "audience", context.ClientId ?? string.Empty
                    },
                });

            var ticket = new AuthenticationTicket(identity, properties);
            context.Validated(ticket);
            return Task.FromResult<object>(null);
        }
    }
}
