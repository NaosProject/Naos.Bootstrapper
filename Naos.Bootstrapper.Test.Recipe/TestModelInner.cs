// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestModelInner.cs" company="Naos Project">
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

    public partial class TestModelInner
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
    }
}