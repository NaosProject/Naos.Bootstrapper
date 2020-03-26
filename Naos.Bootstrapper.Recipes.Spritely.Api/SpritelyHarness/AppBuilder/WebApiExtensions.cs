// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebApiExtensions.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using System;
    using System.Web.Http;
    using Owin;
    using SimpleInjector.Integration.WebApi;

    /// <summary>
    /// Web api extensions for IAppBuilder.
    /// </summary>
    public static class WebApiExtensions
    {
        /// <summary>
        /// Sets up the application to use web API with the specified HTTP configuration initializers.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="httpConfigurationInitializers">The HTTP configuration initializers.</param>
        /// <returns>The modified application.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Disposal will be controlled by Web Api.")]
        public static IAppBuilder UseWebApiWithHttpConfigurationInitializers(this IAppBuilder app, params InitializeHttpConfiguration[] httpConfigurationInitializers)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            var resolver = new AppBuilderServiceResolver(app);
            var container = app.GetContainer();
            var httpConfiguration = new HttpConfiguration
            {
                DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container),
            };

            var initializers = httpConfigurationInitializers ?? new InitializeHttpConfiguration[] { };
            foreach (var initialize in initializers)
            {
                initialize(httpConfiguration, resolver);
            }

            app.UseWebApi(httpConfiguration);

            return app;
        }
    }
}
