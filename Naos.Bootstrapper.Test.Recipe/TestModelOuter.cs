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
}