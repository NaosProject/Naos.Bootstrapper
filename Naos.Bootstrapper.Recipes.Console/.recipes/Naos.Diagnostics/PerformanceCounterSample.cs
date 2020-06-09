﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PerformanceCounterDescription.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in Naos.Recipes source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

#if NaosDiagnosticsDomain
namespace Naos.Diagnostics.Domain
#else
namespace Naos.Diagnostics.Recipes
#endif
{
    using System;
    using OBeautifulCode.Equality.Recipes;
    using static System.FormattableString;

    /// <summary>
    /// Model of a sampled performance counter.
    /// </summary>
#if NaosDiagnosticsRecipes
    [Serializable]
    public class RecipePerformanceCounterSample : IEquatable<RecipePerformanceCounterSample>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecipePerformanceCounterSample"/> class.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="value">The sampled value.</param>
        public RecipePerformanceCounterSample(RecipePerformanceCounterDescription description, float value)
        {
            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            this.Description = description;
            this.Value = value;
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public RecipePerformanceCounterDescription Description { get; private set; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="first">First parameter.</param>
        /// <param name="second">Second parameter.</param>
        /// <returns>A value indicating whether or not the two items are equal.</returns>
        public static bool operator ==(RecipePerformanceCounterSample first, RecipePerformanceCounterSample second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }

            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return false;
            }

            var result = first.Description == second.Description &&
                         first.Value == second.Value;

            return result;
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="first">First parameter.</param>
        /// <param name="second">Second parameter.</param>
        /// <returns>A value indicating whether or not the two items are inequal.</returns>
        public static bool operator !=(RecipePerformanceCounterSample first, RecipePerformanceCounterSample second) => !(first == second);

        /// <inheritdoc />
        public bool Equals(RecipePerformanceCounterSample other) => this == other;

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as RecipePerformanceCounterSample);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.Description)
            .Hash(this.Value)
            .Value;
#elif NaosDiagnosticsDomain
    [Serializable]
    public class PerformanceCounterSample : IEquatable<PerformanceCounterSample>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PerformanceCounterSample"/> class.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="value">The sampled value.</param>
        public PerformanceCounterSample(PerformanceCounterDescription description, float value)
        {
            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            this.Description = description;
            this.Value = value;
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public PerformanceCounterDescription Description { get; private set; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="first">First parameter.</param>
        /// <param name="second">Second parameter.</param>
        /// <returns>A value indicating whether or not the two items are equal.</returns>
        public static bool operator ==(PerformanceCounterSample first, PerformanceCounterSample second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }

            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return false;
            }
        
            var result = first.Description == second.Description &&
                         first.Value == second.Value;

            return result;
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="first">First parameter.</param>
        /// <param name="second">Second parameter.</param>
        /// <returns>A value indicating whether or not the two items are inequal.</returns>
        public static bool operator !=(PerformanceCounterSample first, PerformanceCounterSample second) => !(first == second);

        /// <inheritdoc />
        public bool Equals(PerformanceCounterSample other) => this == other;

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as PerformanceCounterSample);
        
        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.Description)
            .Hash(this.Value)
            .Value;
#else
    [System.CodeDom.Compiler.GeneratedCode("Naos.Diagnostics", "See package version number")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Serializable]
    internal class RecipePerformanceCounterSample : IEquatable<RecipePerformanceCounterSample>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecipePerformanceCounterSample"/> class.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="value">The sampled value.</param>
        public RecipePerformanceCounterSample(RecipePerformanceCounterDescription description, float value)
        {
            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            this.Description = description;
            this.Value = value;
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public RecipePerformanceCounterDescription Description { get; private set; }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="first">First parameter.</param>
        /// <param name="second">Second parameter.</param>
        /// <returns>A value indicating whether or not the two items are equal.</returns>
        public static bool operator ==(RecipePerformanceCounterSample first, RecipePerformanceCounterSample second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }

            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return false;
            }

            var result = first.Description == second.Description &&
                         first.Value == second.Value;

            return result;
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="first">First parameter.</param>
        /// <param name="second">Second parameter.</param>
        /// <returns>A value indicating whether or not the two items are inequal.</returns>
        public static bool operator !=(RecipePerformanceCounterSample first, RecipePerformanceCounterSample second) => !(first == second);

        /// <inheritdoc />
        public bool Equals(RecipePerformanceCounterSample other) => this == other;

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as RecipePerformanceCounterSample);
        
        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.Description)
            .Hash(this.Value)
            .Value;
#endif

        /// <summary>
        /// Gets the value.
        /// </summary>
        public float Value { get; private set; }

        /// <summary>
        /// Gets a value indicating whether or not the value was in range (if range provided).
        /// </summary>
        public bool? InRange => this.Description.ExpectedMinValue == null || this.Description.ExpectedMaxValue == null
                                    ? (bool?)null
                                    : this.Value < this.Description.ExpectedMaxValue && this.Value > this.Description.ExpectedMinValue;

        /// <inheritdoc />
        public override string ToString()
        {
            var result = Invariant($"{this.GetType().Name} - {nameof(this.Description)}: {this.Description}; {nameof(this.Value)}: {this.Value}.");
            return result;
        }
    }
}
