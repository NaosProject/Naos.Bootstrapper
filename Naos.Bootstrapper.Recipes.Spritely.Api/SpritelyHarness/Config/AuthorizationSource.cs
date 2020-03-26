// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthorizationSource.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    /// <summary>
    ///     Set of all possible authorization sources.
    /// </summary>
    public enum AuthorizationSource
    {
        /// <summary>
        /// The header.
        /// </summary>
        Header = 0,

        /// <summary>
        /// The form.
        /// </summary>
        Form,

        /// <summary>
        /// The query string.
        /// </summary>
        QueryString,
    }
}
