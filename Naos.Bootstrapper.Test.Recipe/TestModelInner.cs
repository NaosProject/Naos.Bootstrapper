// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestModelInner.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper.Test.CodeGeneration
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using OBeautifulCode.Collection.Recipes;
    using OBeautifulCode.Math.Recipes;
    using OBeautifulCode.Type;
    using OBeautifulCode.Validation.Recipes;
    using static System.FormattableString;

    [SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes", Justification = "Just to use as a key in dictionary, don't really need it.")]
    public partial class TestModelInner : IComparable<TestModelInner>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "Name/spelling is correct.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "integer", Justification = "Name/spelling is correct.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "boolean", Justification = "Name/spelling is correct.")]
        public TestModelInner(
            bool booleanProperty,
            int integerProperty,
            string stringProperty,
            IReadOnlyDictionary<string, string> readOnlyDictionaryOfStringString,
            IReadOnlyCollection<string> readOnlyCollectionOfString)
        {
            new { stringProperty }.Must().NotBeNullNorWhiteSpace();
            new { readOnlyDictionaryOfStringString }.Must().NotBeNull();
            new { readOnlyCollectionOfString }.Must().NotBeNull();

            this.BooleanProperty = booleanProperty;
            this.IntegerProperty = integerProperty;
            this.StringProperty = stringProperty;
            this.ReadOnlyDictionaryOfStringString = readOnlyDictionaryOfStringString;
            this.ReadOnlyCollectionOfString = readOnlyCollectionOfString;
        }

        public bool BooleanProperty { get; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "integer", Justification = "Name/spelling is correct.")]
        public int IntegerProperty { get; }

        public string StringProperty { get; }

        public IReadOnlyDictionary<string, string> ReadOnlyDictionaryOfStringString { get; }

        public IReadOnlyCollection<string> ReadOnlyCollectionOfString { get; }

        /// <inheritdoc />
        public int CompareTo(
            TestModelInner other)
        {
            if (other == null)
            {
                return -1;
            }

            return this.IntegerProperty.CompareTo(other.IntegerProperty);
        }
    }

    public partial class TestModelInner : IModel<TestModelInner>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="TestModelInner"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>True if the two items are equal; otherwise false.</returns>
        public static bool operator ==(TestModelInner left, TestModelInner right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            var result = left.BooleanProperty == right.BooleanProperty
                      && left.IntegerProperty == right.IntegerProperty
                      && left.StringProperty.Equals(right.StringProperty, StringComparison.Ordinal)
                      && left.ReadOnlyDictionaryOfStringString.DictionaryEqual(right.ReadOnlyDictionaryOfStringString)
                      && left.ReadOnlyCollectionOfString.SequenceEqualHandlingNulls(right.ReadOnlyCollectionOfString);

            return result;
        }

        /// <summary>
        /// Determines whether two objects of type <see cref="TestModelInner"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>True if the two items not equal; otherwise false.</returns>
        public static bool operator !=(TestModelInner left, TestModelInner right) => !(left == right);

        /// <inheritdoc />
        public bool Equals(TestModelInner other) => this == other;

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as TestModelInner);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.BooleanProperty)
            .Hash(this.IntegerProperty)
            .Hash(this.StringProperty)
            .HashDictionary(this.ReadOnlyDictionaryOfStringString)
            .HashElements(this.ReadOnlyCollectionOfString)
            .Value;

        /// <inheritdoc />
        public object Clone() => this.DeepClone();

        /// <inheritdoc />
        public TestModelInner DeepClone()
        {
            var result = new TestModelInner(
                                 this.BooleanProperty,
                                 this.IntegerProperty,
                                 this.StringProperty?.Clone().ToString(),
                                 this.ReadOnlyDictionaryOfStringString?.ToDictionary(k => k.Key?.Clone().ToString(), v => v.Value?.Clone().ToString()),
                                 this.ReadOnlyCollectionOfString?.Select(_ => _?.Clone().ToString()).ToList());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <paramref name="booleanProperty" />.
        /// </summary>
        /// <param name="booleanProperty">The new <see cref="BooleanProperty" />.</param>
        /// <returns>New <see cref="TestModelInner" /> using the specified <paramref name="booleanProperty" /> for <see cref="BooleanProperty" /> and a deep clone of every other property.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "boolean", Justification = "Name is correct.")]
        public TestModelInner DeepCloneWithBooleanProperty(bool booleanProperty)
        {
            var result = new TestModelInner(
                                 booleanProperty,
                                 this.IntegerProperty,
                                 this.StringProperty?.Clone().ToString(),
                                 this.ReadOnlyDictionaryOfStringString?.ToDictionary(k => k.Key?.Clone().ToString(), v => v.Value?.Clone().ToString()),
                                 this.ReadOnlyCollectionOfString?.Select(_ => _?.Clone().ToString()).ToList());
            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <paramref name="integerProperty" />.
        /// </summary>
        /// <param name="integerProperty">The new <see cref="IntegerProperty" />.</param>
        /// <returns>New <see cref="TestModelInner" /> using the specified <paramref name="integerProperty" /> for <see cref="IntegerProperty" /> and a deep clone of every other property.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "integer", Justification = "Name is correct.")]
        public TestModelInner DeepCloneWithIntegerProperty(int integerProperty)
        {
            var result = new TestModelInner(
                                 this.BooleanProperty,
                                 integerProperty,
                                 this.StringProperty?.Clone().ToString(),
                                 this.ReadOnlyDictionaryOfStringString?.ToDictionary(k => k.Key?.Clone().ToString(), v => v.Value?.Clone().ToString()),
                                 this.ReadOnlyCollectionOfString?.Select(_ => _?.Clone().ToString()).ToList());
            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <paramref name="stringProperty" />.
        /// </summary>
        /// <param name="stringProperty">The new <see cref="StringProperty" />.</param>
        /// <returns>New <see cref="TestModelInner" /> using the specified <paramref name="stringProperty" /> for <see cref="StringProperty" /> and a deep clone of every other property.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "Name is correct.")]
        public TestModelInner DeepCloneWithStringProperty(string stringProperty)
        {
            var result = new TestModelInner(
                                 this.BooleanProperty,
                                 this.IntegerProperty,
                                 stringProperty,
                                 this.ReadOnlyDictionaryOfStringString?.ToDictionary(k => k.Key?.Clone().ToString(), v => v.Value?.Clone().ToString()),
                                 this.ReadOnlyCollectionOfString?.Select(_ => _?.Clone().ToString()).ToList());
            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <paramref name="readOnlyDictionaryOfStringString" />.
        /// </summary>
        /// <param name="readOnlyDictionaryOfStringString">The new <see cref="ReadOnlyDictionaryOfStringString" />.</param>
        /// <returns>New <see cref="TestModelInner" /> using the specified <paramref name="readOnlyDictionaryOfStringString" /> for <see cref="ReadOnlyDictionaryOfStringString" /> and a deep clone of every other property.</returns>
        public TestModelInner DeepCloneWithReadOnlyDictionaryOfStringString(IReadOnlyDictionary<string, string> readOnlyDictionaryOfStringString)
        {
            var result = new TestModelInner(
                                 this.BooleanProperty,
                                 this.IntegerProperty,
                                 this.StringProperty?.Clone().ToString(),
                                 readOnlyDictionaryOfStringString,
                                 this.ReadOnlyCollectionOfString?.Select(_ => _?.Clone().ToString()).ToList());
            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <paramref name="readOnlyCollectionOfString" />.
        /// </summary>
        /// <param name="readOnlyCollectionOfString">The new <see cref="ReadOnlyCollectionOfString" />.</param>
        /// <returns>New <see cref="TestModelInner" /> using the specified <paramref name="readOnlyCollectionOfString" /> for <see cref="ReadOnlyCollectionOfString" /> and a deep clone of every other property.</returns>
        public TestModelInner DeepCloneWithReadOnlyCollectionOfString(IReadOnlyCollection<string> readOnlyCollectionOfString)
        {
            var result = new TestModelInner(
                                 this.BooleanProperty,
                                 this.IntegerProperty,
                                 this.StringProperty?.Clone().ToString(),
                                 this.ReadOnlyDictionaryOfStringString?.ToDictionary(k => k.Key?.Clone().ToString(), v => v.Value?.Clone().ToString()),
                                 readOnlyCollectionOfString);
            return result;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var result = Invariant($"{nameof(Naos.Bootstrapper.Test.CodeGeneration)}.{nameof(TestModelInner)}: BooleanProperty = {this.BooleanProperty.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, IntegerProperty = {this.IntegerProperty.ToString(CultureInfo.InvariantCulture) ?? "<null>"}, StringProperty = {this.StringProperty?.ToString() ?? "<null>"}, ReadOnlyDictionaryOfStringString = {this.ReadOnlyDictionaryOfStringString?.ToString() ?? "<null>"}, ReadOnlyCollectionOfString = {this.ReadOnlyCollectionOfString?.ToString() ?? "<null>"}.");

            return result;
        }
    }
}