// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HandlerFactory.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in 'Naos.Bootstrapper.Recipes.Hangfire.Console' source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper.Recipes.Hangfire.Console
{
    using System;
    using System.Collections.Generic;

    using Naos.MessageBus.Domain;

    /// <summary>
    /// Example implementation to get compiling.
    /// </summary>
    public static partial class HandlerFactory
    {
        /// <summary>
        /// Map of the message type to the intended handler type.  Must have a parameterless constructor and implement <see cref="IHandleMessages" />,
        /// however deriving from <see cref="MessageHandlerBase{T}" /> is recommended as it's more straightforward and easier to write.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "This is used in real usage, keeping here as a reference example to make it easier to get started.")]
        private static readonly IReadOnlyDictionary<Type, Type> MessageTypeToHandlerTypeMap = HandlerFactory.DiscoverHandlersInAssemblies(new[] { typeof(ExampleMessage).Assembly, typeof(ExampleMessageHandler).Assembly, });
    }
}