// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Delegates.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using System.Web.Http;
    using SimpleInjector;

    /// <summary>
    /// Delegate abstracting the write logging function.
    /// </summary>
    /// <param name="message">The message.</param>
    public delegate void WriteLog(string message);

    /// <summary>
    /// Delegate representing a container initialization function.
    /// </summary>
    /// <param name="container">The container.</param>
    public delegate void InitializeContainer(Container container);

    /// <summary>
    /// Delegate representing log policy initialization function.
    /// </summary>
    public delegate void InitializeLogPolicy();

    /// <summary>
    /// Delegate representing an HTTP configuration initialization function.
    /// </summary>
    /// <param name="httpConfiguration">The HTTP configuration.</param>
    /// <param name="serviceResolver">The service resolver.</param>
    public delegate void InitializeHttpConfiguration(HttpConfiguration httpConfiguration, IServiceResolver serviceResolver);
}
