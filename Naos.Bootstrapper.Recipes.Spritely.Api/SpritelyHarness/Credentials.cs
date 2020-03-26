// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Credentials.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    /// <summary>
    /// A set of credentials to use to sign in a user.
    /// </summary>
    public class Credentials
    {
        /// <summary>
        /// Gets or sets the type of the authentication that should be used when creating the ClaimsIdentity.
        /// </summary>
        /// <value>The type of the authentication.</value>
        public string AuthenticationType { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }
    }
}
