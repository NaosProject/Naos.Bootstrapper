﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionHelper.Method.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Reflection.Recipes source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Reflection.Recipes
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Reflection;

    using static global::System.FormattableString;

#if !OBeautifulCodeReflectionSolution
    internal
#else
    public
#endif
    static partial class ReflectionHelper
    {
        /// <summary>
        /// Gets the specified interface type's methods along with the methods of all implemented interfaces.
        /// </summary>
        /// <param name="interfaceType">The type of the interface.</param>
        /// <returns>
        /// The methods declared on the specified interface along with the methods of all implemented interfaces.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="interfaceType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="interfaceType"/> is not an interface type.</exception>
        public static IReadOnlyCollection<MethodInfo> GetInterfaceDeclaredAndImplementedMethods(
            this Type interfaceType)
        {
            if (interfaceType == null)
            {
                throw new ArgumentNullException(nameof(interfaceType));
            }

            if (!interfaceType.IsInterface)
            {
                throw new ArgumentException(Invariant($"{nameof(interfaceType)} is not an interface type."));
            }

            var result = new Type[0]
                .Concat(new[] { interfaceType })
                .Concat(interfaceType.GetInterfaces())
                .SelectMany(_ => _.GetMethods(BindingFlagsFor.PublicDeclaredButNotInheritedInstanceMembers))
                .ToList();

            return result;
        }
    }
}