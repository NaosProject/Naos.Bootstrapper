﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NaosConfigurationExtensions.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Naos.Configuration.Domain;
    using Owin;
    using SimpleInjector;

    /// <summary>
    /// Its.Configuration extensions for IAppBuilder.
    /// </summary>
    public static class NaosConfigurationExtensions
    {
        /// <summary>
        /// Adds a settings container initializer to the application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="requireAssemblies">Any assemblies that are not loaded by default that contain classes used as Its.Configuration settings.</param>
        /// <returns>The modified application.</returns>
        public static IAppBuilder UseSettingsContainerInitializer(this IAppBuilder app, params Assembly[] requireAssemblies)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            var allTypes = GetAllTypes(requireAssemblies).ToList();

            void InitializeContainer(Container container)
            {
                foreach (var f in Config.GetFiles())
                {
                    var typeName = f.Name.Substring(0, f.Name.Length - f.Extension.Length);

                    try
                    {
                        // This will throw InvalidOperationException if assembly isn't loaded.
                        var type = allTypes.First(t => string.Equals(t.Name, typeName, StringComparison.OrdinalIgnoreCase));

                        container.Register(type, () => Config.Get(type));
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, Messages.Exception_UseSettingsContainerInitializer_NoType, typeName), innerException: ex);
                    }
                }
            }

            return app.UseContainerInitializer(InitializeContainer);
        }

        private static IEnumerable<Type> GetAllTypes(params Assembly[] assemblies)
        {
            var allTypes = AppDomain.CurrentDomain.GetAssemblies().Concat(assemblies)
                .Where(a => !a.IsDynamic)
                .Where(a => !a.GlobalAssemblyCache)
                .SelectMany(a =>
                {
                    try
                    {
                        return a.GetExportedTypes();
                    }
                    catch (TypeLoadException)
                    {
                    }
                    catch (ReflectionTypeLoadException)
                    {
                    }
                    catch (FileNotFoundException)
                    {
                    }
                    catch (FileLoadException)
                    {
                    }

                    return Enumerable.Empty<Type>();
                });

            return allTypes;
        }
    }
}
