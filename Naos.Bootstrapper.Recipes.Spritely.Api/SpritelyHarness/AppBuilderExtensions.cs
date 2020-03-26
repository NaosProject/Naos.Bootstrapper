// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppBuilderExtensions.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using System;
    using Microsoft.Owin.Security.OAuth;
    using Naos.Bootstrapper.Recipes.Spritely.Api.Internal;
    using Owin;

    /// <summary>
    /// Extensions for IAppBuilder.
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Adds JWT OAuth server container initializer to the application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>The modified application.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Auth", Justification = NaosSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Jwt", Justification = "Spelling/name is correct.")]
        public static IAppBuilder UseJwtOAuthServerContainerInitializer(this IAppBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            InitializeContainer initializeContainer =
                container =>
                {
                    container.Register<IOAuthAuthorizationServerProvider, JwtOAuthClientValidatingServerProvider>();
                };

            return app.UseContainerInitializer(initializeContainer);
        }

        /// <summary>
        /// Sets up the application to uses an JWT OAuth authorization server.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>The modified application.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Auth", Justification = NaosSuppressBecause.CA1704_IdentifiersShouldBeSpelledCorrectly_SpellingIsCorrectInContextOfTheDomain)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Jwt", Justification = "Spelling/name is correct.")]
        public static IAppBuilder UseJwtOAuthServer(this IAppBuilder app)
        {
            var serverOptions = app.GetInstance<JwtOAuthServerOptions>();
            return app.UseOAuthAuthorizationServer(serverOptions);
        }
    }
}
