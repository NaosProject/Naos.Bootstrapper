// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAuthenticator.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using System.Security.Claims;

    /// <summary>
    /// Represents a class that can authenticate users.
    /// </summary>
    public interface IAuthenticator
    {
        /// <summary>
        /// Signs in a user using the specified credentials.
        /// </summary>
        /// <param name="credentials">The credentials.</param>
        /// <returns>The claims identity for the user or null.</returns>
        ClaimsIdentity SignIn(Credentials credentials);
    }
}
