﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RelativeFileCertificate.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using System;
    using System.IO;
    using System.Security;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// Describes how to load a file-based certificate.
    /// </summary>
    public class RelativeFileCertificate
    {
        /// <summary>
        /// Gets or sets the key storage flags.
        /// </summary>
        /// <value>
        /// The key storage flags.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flags", Justification = "Name is designed to match underlying type.")]
        public X509KeyStorageFlags KeyStorageFlags { get; set; }

        /// <summary>
        /// Gets or sets the optional base path. If unset will use the current directory.
        /// </summary>
        /// <value>
        /// The base path.
        /// </value>
        public string BasePath { get; set; }

        /// <summary>
        /// Gets or sets the relative certificate file path.
        /// </summary>
        /// <value>The relative certificate file path.</value>
        public string RelativeFilePath { get; set; }

        /// <summary>
        /// Gets or sets the certificate password.
        /// </summary>
        /// <value>The certificate password.</value>
        public SecureString Password { get; set; }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        public string FilePath
        {
            get
            {
                var basePath = this.BasePath ?? Environment.CurrentDirectory;
                var fullPath = Path.Combine(basePath, this.RelativeFilePath);

                return fullPath;
            }
        }
    }
}