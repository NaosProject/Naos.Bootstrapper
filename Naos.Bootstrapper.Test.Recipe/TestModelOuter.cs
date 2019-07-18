// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestModelOuter.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper.Test.CodeGeneration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OBeautifulCode.Collection.Recipes;
    using OBeautifulCode.Math.Recipes;
    using OBeautifulCode.Type;
    using OBeautifulCode.Validation.Recipes;
    using static System.FormattableString;

    public partial class TestModelOuter
    {
        public TestModelOuter(
            TestModelInner testModelInnerProperty,
            IReadOnlyDictionary<TestModelInner, TestModelInner> readOnlyDictionaryOfTestModelInnerTestModelInner,
            IReadOnlyCollection<TestModelInner> readOnlyCollectionOfTestModelInner)
        {
            testModelInnerProperty.Named(nameof(testModelInnerProperty)).Must().NotBeNull();
            readOnlyDictionaryOfTestModelInnerTestModelInner.Named(nameof(readOnlyDictionaryOfTestModelInnerTestModelInner)).Must().NotBeNull();
            readOnlyCollectionOfTestModelInner.Named(nameof(readOnlyCollectionOfTestModelInner)).Must().NotBeNull();

            this.TestModelInnerProperty = testModelInnerProperty;
            this.ReadOnlyDictionaryOfTestModelInnerTestModelInner = readOnlyDictionaryOfTestModelInnerTestModelInner;
            this.ReadOnlyCollectionOfTestModelInner = readOnlyCollectionOfTestModelInner;
        }

        public TestModelInner TestModelInnerProperty { get; }

        public IReadOnlyDictionary<TestModelInner, TestModelInner> ReadOnlyDictionaryOfTestModelInnerTestModelInner { get; }

        public IReadOnlyCollection<TestModelInner> ReadOnlyCollectionOfTestModelInner { get; }
    }

    public partial class TestModelOuter : IModel<TestModelOuter>
    {
        /// <summary>
        /// Determines whether two objects of type <see cref="TestModelOuter"/> are equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>True if the two items are equal; otherwise false.</returns>
        public static bool operator ==(TestModelOuter left, TestModelOuter right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            var result = left.TestModelInnerProperty == right.TestModelInnerProperty
                      && left.ReadOnlyDictionaryOfTestModelInnerTestModelInner.DictionaryEqual(right.ReadOnlyDictionaryOfTestModelInnerTestModelInner)
                      && left.ReadOnlyCollectionOfTestModelInner.SequenceEqualHandlingNulls(right.ReadOnlyCollectionOfTestModelInner);

            return result;
        }

        /// <summary>
        /// Determines whether two objects of type <see cref="TestModelOuter"/> are not equal.
        /// </summary>
        /// <param name="left">The object to the left of the operator.</param>
        /// <param name="right">The object to the right of the operator.</param>
        /// <returns>True if the two items not equal; otherwise false.</returns>
        public static bool operator !=(TestModelOuter left, TestModelOuter right) => !(left == right);

        /// <inheritdoc />
        public bool Equals(TestModelOuter other) => this == other;

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as TestModelOuter);

        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            .Hash(this.TestModelInnerProperty)
            .HashDictionary(this.ReadOnlyDictionaryOfTestModelInnerTestModelInner)
            .HashElements(this.ReadOnlyCollectionOfTestModelInner)
            .Value;

        /// <inheritdoc />
        public object Clone() => this.DeepClone();

        /// <inheritdoc />
        public TestModelOuter DeepClone()
        {
            var result = new TestModelOuter(
                                 this.TestModelInnerProperty?.DeepClone(),
                                 this.ReadOnlyDictionaryOfTestModelInnerTestModelInner?.ToDictionary(k => k.Key?.DeepClone(), v => v.Value?.DeepClone()),
                                 this.ReadOnlyCollectionOfTestModelInner?.Select(_ => _?.DeepClone()).ToList());

            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <paramref name="testModelInnerProperty" />.
        /// </summary>
        /// <param name="testModelInnerProperty">The new <see cref="TestModelInnerProperty" />.</param>
        /// <returns>New <see cref="TestModelOuter" /> using the specified <paramref name="testModelInnerProperty" /> for <see cref="TestModelInnerProperty" /> and a deep clone of every other property.</returns>
        public TestModelOuter DeepCloneWithTestModelInnerProperty(TestModelInner testModelInnerProperty)
        {
            var result = new TestModelOuter(
                                 testModelInnerProperty,
                                 this.ReadOnlyDictionaryOfTestModelInnerTestModelInner?.ToDictionary(k => k.Key?.DeepClone(), v => v.Value?.DeepClone()),
                                 this.ReadOnlyCollectionOfTestModelInner?.Select(_ => _?.DeepClone()).ToList());
            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <paramref name="readOnlyDictionaryOfTestModelInnerTestModelInner" />.
        /// </summary>
        /// <param name="readOnlyDictionaryOfTestModelInnerTestModelInner">The new <see cref="ReadOnlyDictionaryOfTestModelInnerTestModelInner" />.</param>
        /// <returns>New <see cref="TestModelOuter" /> using the specified <paramref name="readOnlyDictionaryOfTestModelInnerTestModelInner" /> for <see cref="ReadOnlyDictionaryOfTestModelInnerTestModelInner" /> and a deep clone of every other property.</returns>
        public TestModelOuter DeepCloneWithReadOnlyDictionaryOfTestModelInnerTestModelInner(IReadOnlyDictionary<TestModelInner, TestModelInner> readOnlyDictionaryOfTestModelInnerTestModelInner)
        {
            var result = new TestModelOuter(
                                 this.TestModelInnerProperty?.DeepClone(),
                                 readOnlyDictionaryOfTestModelInnerTestModelInner,
                                 this.ReadOnlyCollectionOfTestModelInner?.Select(_ => _?.DeepClone()).ToList());
            return result;
        }

        /// <summary>
        /// Deep clones this object with a new <paramref name="readOnlyCollectionOfTestModelInner" />.
        /// </summary>
        /// <param name="readOnlyCollectionOfTestModelInner">The new <see cref="ReadOnlyCollectionOfTestModelInner" />.</param>
        /// <returns>New <see cref="TestModelOuter" /> using the specified <paramref name="readOnlyCollectionOfTestModelInner" /> for <see cref="ReadOnlyCollectionOfTestModelInner" /> and a deep clone of every other property.</returns>
        public TestModelOuter DeepCloneWithReadOnlyCollectionOfTestModelInner(IReadOnlyCollection<TestModelInner> readOnlyCollectionOfTestModelInner)
        {
            var result = new TestModelOuter(
                                 this.TestModelInnerProperty?.DeepClone(),
                                 this.ReadOnlyDictionaryOfTestModelInnerTestModelInner?.ToDictionary(k => k.Key?.DeepClone(), v => v.Value?.DeepClone()),
                                 readOnlyCollectionOfTestModelInner);
            return result;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var result = Invariant($"{nameof(Naos.Bootstrapper.Test.CodeGeneration)}.{nameof(TestModelOuter)}: TestModelInnerProperty = {this.TestModelInnerProperty?.ToString() ?? "<null>"}, ReadOnlyDictionaryOfTestModelInnerTestModelInner = {this.ReadOnlyDictionaryOfTestModelInnerTestModelInner?.ToString() ?? "<null>"}, ReadOnlyCollectionOfTestModelInner = {this.ReadOnlyCollectionOfTestModelInner?.ToString() ?? "<null>"}.");

            return result;
        }
    }
}