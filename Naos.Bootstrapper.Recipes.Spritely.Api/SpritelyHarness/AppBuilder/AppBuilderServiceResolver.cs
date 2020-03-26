// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppBuilderServiceResolver.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using System;
    using Owin;

    /// <summary>
    /// A default implementation for resolving instances from the OWIN AppBuilder.
    /// </summary>
    public class AppBuilderServiceResolver : IServiceResolver
    {
        private readonly IAppBuilder app;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppBuilderServiceResolver"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="ArgumentNullException">If app is null.</exception>
        public AppBuilderServiceResolver(IAppBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            this.app = app;
        }

        /// <summary>
        /// Gets an instance by its service type.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>
        /// The service instance.
        /// </returns>
        public TService GetInstance<TService>()
            where TService : class
        {
            return this.app.GetInstance<TService>();
        }

        /// <summary>
        /// Gets the instance by its service type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>
        /// The service instance.
        /// </returns>
        public object GetInstance(Type serviceType)
        {
            return this.app.GetInstance(serviceType);
        }
    }
}
