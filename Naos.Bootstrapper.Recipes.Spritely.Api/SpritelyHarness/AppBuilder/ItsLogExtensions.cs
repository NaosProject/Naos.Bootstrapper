// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ItsLogExtensions.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using System;
    using Its.Log.Instrumentation;
    using Owin;

    /// <summary>
    /// Its.Log extensions for IAppBuilder.
    /// </summary>
    public static class ItsLogExtensions
    {
        /// <summary>
        /// Uses the request and response logging.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>The modified application.</returns>
        /// <exception cref="ArgumentNullException">If app is null.</exception>
        public static IAppBuilder UseRequestAndResponseLogging(this IAppBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            app.Use(async (context, next) =>
            {
                Log.Write(context.Request);

                await next.Invoke();

                Log.Write(context.Response);
            });

            return app;
        }
    }
}
