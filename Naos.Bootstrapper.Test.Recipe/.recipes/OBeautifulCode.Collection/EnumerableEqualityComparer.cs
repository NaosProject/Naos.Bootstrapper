﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumerableEqualityComparer.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.Collection source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Collection.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using OBeautifulCode.Math.Recipes;

    using static System.FormattableString;

    /// <summary>
    /// An implementation of <see cref="IEqualityComparer{T}"/> for any <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <remarks>
    /// Adapted from: <a href="https://stackoverflow.com/a/14675741/356790" />.
    /// </remarks>
    /// <typeparam name="T">The type of objects to enumerate.</typeparam>
#if !OBeautifulCodeCollectionRecipesProject
    [ExcludeFromCodeCoverage]
    [System.CodeDom.Compiler.GeneratedCode("OBeautifulCode.Collection", "See package version number")]
    internal
#else
    public
#endif
        class EnumerableEqualityComparer<T> : IEqualityComparer<IEnumerable<T>>
    {
        private readonly EnumerableEqualityComparerStrategy enumerableEqualityComparerStrategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumerableEqualityComparer{T}"/> class.
        /// </summary>
        /// <param name="enumerableEqualityComparerStrategy">The strategy to use when comparing two <see cref="IEnumerable{T}"/> for equality.</param>
        public EnumerableEqualityComparer(
            EnumerableEqualityComparerStrategy enumerableEqualityComparerStrategy = EnumerableEqualityComparerStrategy.SequenceEqual)
        {
            this.enumerableEqualityComparerStrategy = enumerableEqualityComparerStrategy;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "This is an appropriate exception to raise.")]
        public bool Equals(
            IEnumerable<T> x,
            IEnumerable<T> y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            {
                return false;
            }

            bool result;

            switch (this.enumerableEqualityComparerStrategy)
            {
                case EnumerableEqualityComparerStrategy.SequenceEqual:
                    result = x.SequenceEqual(y);
                    break;
                case EnumerableEqualityComparerStrategy.NoSymmetricDifference:
                    result = !x.SymmetricDifference(y).Any();
                    break;
                default:
                    throw new NotSupportedException(Invariant($"This {nameof(EnumerableEqualityComparerStrategy)} is not supported: {this.enumerableEqualityComparerStrategy}."));
            }

            return result;
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification = "This is an appropriate exception to raise.")]
        public int GetHashCode(
            IEnumerable<T> obj)
        {
            int result;

            var hashCodeHelper = HashCodeHelper.Initialize();

            switch (this.enumerableEqualityComparerStrategy)
            {
                case EnumerableEqualityComparerStrategy.SequenceEqual:
                    result = hashCodeHelper.HashElements(obj).Value;
                    break;
                case EnumerableEqualityComparerStrategy.NoSymmetricDifference:
                    result = hashCodeHelper.HashElements(obj.Distinct().OrderBy(_ => _)).Value;
                    break;
                default:
                    throw new NotSupportedException(Invariant($"This {nameof(EnumerableEqualityComparerStrategy)} is not supported: {this.enumerableEqualityComparerStrategy}."));
            }

            return result;
        }
    }
}