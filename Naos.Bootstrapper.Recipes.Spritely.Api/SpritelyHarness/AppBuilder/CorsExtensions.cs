// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CorsExtensions.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Cors;
    using Microsoft.Owin.Cors;
    using Owin;

    /// <summary>
    /// Its.Configuration extensions for IAppBuilder.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cors", Justification = "Spelling/name is correct.")]
    public static class CorsExtensions
    {
        /// <summary>
        /// Uses the cors.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>IAppBuilder.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cors", Justification = "Spelling/name is correct.")]
        public static IAppBuilder UseCors(this IAppBuilder app)
        {
            var hostingSettings = app.GetInstance<HostingSettings>();
            var cors = hostingSettings.Cors;

            if (cors == null || !cors.Origins.Any())
            {
                throw new InvalidOperationException(Messages.Exception_UseCors_NoSettingsProvided);
            }

            var corsPolicy = new CorsPolicy
            {
                SupportsCredentials = cors.SupportsCredentials,
                PreflightMaxAge = cors.PreflightMaxAge,
            };

            cors.Origins.ToList().ForEach(corsPolicy.Origins.Add);
            cors.Headers.ToList().ForEach(corsPolicy.Headers.Add);
            cors.Methods.ToList().ForEach(corsPolicy.Methods.Add);
            cors.ExposedHeaders.ToList().ForEach(corsPolicy.ExposedHeaders.Add);

            if (cors.Origins.FirstOrDefault(o => o == "*") != null)
            {
                corsPolicy.AllowAnyOrigin = true;
            }

            if (cors.Headers.FirstOrDefault(h => h == "*") != null)
            {
                corsPolicy.AllowAnyHeader = true;
            }

            if (cors.Methods.FirstOrDefault(m => m == "*") != null)
            {
                corsPolicy.AllowAnyMethod = true;
            }

            app.UseCors(new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = request => Task.FromResult(corsPolicy),
                },
            });

            return app;
        }
    }
}
