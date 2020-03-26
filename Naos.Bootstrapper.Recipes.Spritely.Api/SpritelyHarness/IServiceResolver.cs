// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IServiceResolver.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using System;

    /// <summary>
    /// Represents an object capable of resolving service instances.
    /// </summary>
    public interface IServiceResolver
    {
        /// <summary>
        /// Gets an instance by its service type.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns>The service instance.</returns>
        TService GetInstance<TService>()
            where TService : class;

        /// <summary>
        /// Gets the instance by its service type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>The service instance.</returns>
        object GetInstance(Type serviceType);
    }
}
