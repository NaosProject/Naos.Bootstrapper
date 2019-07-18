// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BootstrapperDummyFactory.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper.Test
{
    using System;
    using System.Collections.Generic;

    using FakeItEasy;

    using OBeautifulCode.AutoFakeItEasy;

    /// <summary>
    /// Dummy construction.
    /// </summary>
    public class BootstrapperDummyFactory : IDummyFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BootstrapperDummyFactory"/> class.
        /// </summary>
        public BootstrapperDummyFactory()
        {
        }

        /// <inheritdoc />
        public Priority Priority => new Priority(1);

        /// <inheritdoc />
        public bool CanCreate(Type type)
        {
            return false;
        }

        /// <inheritdoc />
        public object Create(Type type)
        {
            return null;
        }
    }
}