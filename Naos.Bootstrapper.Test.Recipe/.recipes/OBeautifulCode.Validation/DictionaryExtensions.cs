﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DictionaryExtensions.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Validation source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Validation.Recipes
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using static System.FormattableString;

    /// <summary>
    /// Extension methods on type <see cref="IDictionary{TKey, TValue}"/> and <see cref="IReadOnlyDictionary{TKey, TValue}" />.
    /// </summary>
#if !OBeautifulCodeValidationRecipesProject
    [System.Diagnostics.DebuggerStepThrough]
    [ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.Validation", "See package version number")]
    internal
#else
    public
#endif
        static class DictionaryExtensions
    {
        /// <summary>
        /// Converts a generic dictionary to a non-generic dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
        /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
        /// <param name="value">The dictionary to convert.</param>
        /// <returns>
        /// The specified generic dictionary converted to a non-generic dictionary.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/>is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="value"/> contains duplicate keys.</exception>
        public static IDictionary ToNonGenericDictionary<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> value)
        {
            new { value }.Must().NotBeNull();

            var result = new Hashtable();
            foreach (var item in value)
            {
                if (result.ContainsKey(item.Key))
                {
                    throw new ArgumentException(Invariant($"{nameof(value)} contains duplicate keys."), nameof(value));
                }

                result.Add(item.Key, item.Value);
            }

            return result;
        }
    }
}