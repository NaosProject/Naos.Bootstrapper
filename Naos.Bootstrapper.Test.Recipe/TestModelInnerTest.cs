// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestModelInnerTest.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper.Test.CodeGeneration.TestModelInnerTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using Castle.DynamicProxy.Internal;
    using FakeItEasy;
    using FluentAssertions;
    using OBeautifulCode.AutoFakeItEasy;
    using OBeautifulCode.Collection.Recipes;
    using OBeautifulCode.Representation;
    using OBeautifulCode.Type;
    using Xunit;
    using Xunit.Abstractions;

    using static System.FormattableString;

    public partial class TestModelInnerTest
    {
        private readonly ITestOutputHelper testOutputHelper;

        public TestModelInnerTest(
            ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void GenerateModel()
        {
            var results = CodeGenerator.GenerateForModel<TestModelInner>(CodeGenerator.GenerateFor.ModelImplementationPartialClass);
            this.testOutputHelper.WriteLine(results);
        }

        [Fact]
        public void GenerateTests()
        {
            var results = CodeGenerator.GenerateForModel<TestModelInner>(
                CodeGenerator.GenerateFor.ModelImplementationTestsPartialClassWithoutSerialization);
            this.testOutputHelper.WriteLine(results);
        }
    }

    public partial class TestModelInnerTest
    {
        private static readonly TestModelInner ObjectForEquatableTests = A.Dummy<TestModelInner>();

        private static readonly TestModelInner ObjectThatIsEqualToButNotTheSameAsObjectForEquatableTests =
            new TestModelInner(
                                 ObjectForEquatableTests.BooleanProperty,
                                 ObjectForEquatableTests.IntegerProperty,
                                 ObjectForEquatableTests.StringProperty,
                                 ObjectForEquatableTests.ReadOnlyDictionaryOfStringString,
                                 ObjectForEquatableTests.ReadOnlyCollectionOfString);

        private static readonly TestModelInner[] ObjectsThatAreNotEqualToObjectForEquatableTests =
        {
            new TestModelInner(
                                 ObjectForEquatableTests.BooleanProperty,
                                 A.Dummy<int>().ThatIsNot(ObjectForEquatableTests.IntegerProperty),
                                 A.Dummy<string>().ThatIsNot(ObjectForEquatableTests.StringProperty),
                                 A.Dummy<IReadOnlyDictionary<string, string>>().ThatIsNot(ObjectForEquatableTests.ReadOnlyDictionaryOfStringString),
                                 A.Dummy<IReadOnlyCollection<string>>().ThatIsNot(ObjectForEquatableTests.ReadOnlyCollectionOfString)),
            new TestModelInner(
                                 A.Dummy<bool>().ThatIsNot(ObjectForEquatableTests.BooleanProperty),
                                 ObjectForEquatableTests.IntegerProperty,
                                 A.Dummy<string>().ThatIsNot(ObjectForEquatableTests.StringProperty),
                                 A.Dummy<IReadOnlyDictionary<string, string>>().ThatIsNot(ObjectForEquatableTests.ReadOnlyDictionaryOfStringString),
                                 A.Dummy<IReadOnlyCollection<string>>().ThatIsNot(ObjectForEquatableTests.ReadOnlyCollectionOfString)),
            new TestModelInner(
                                 A.Dummy<bool>().ThatIsNot(ObjectForEquatableTests.BooleanProperty),
                                 A.Dummy<int>().ThatIsNot(ObjectForEquatableTests.IntegerProperty),
                                 ObjectForEquatableTests.StringProperty,
                                 A.Dummy<IReadOnlyDictionary<string, string>>().ThatIsNot(ObjectForEquatableTests.ReadOnlyDictionaryOfStringString),
                                 A.Dummy<IReadOnlyCollection<string>>().ThatIsNot(ObjectForEquatableTests.ReadOnlyCollectionOfString)),
            new TestModelInner(
                                 A.Dummy<bool>().ThatIsNot(ObjectForEquatableTests.BooleanProperty),
                                 A.Dummy<int>().ThatIsNot(ObjectForEquatableTests.IntegerProperty),
                                 A.Dummy<string>().ThatIsNot(ObjectForEquatableTests.StringProperty),
                                 ObjectForEquatableTests.ReadOnlyDictionaryOfStringString,
                                 A.Dummy<IReadOnlyCollection<string>>().ThatIsNot(ObjectForEquatableTests.ReadOnlyCollectionOfString)),
            new TestModelInner(
                                 A.Dummy<bool>().ThatIsNot(ObjectForEquatableTests.BooleanProperty),
                                 A.Dummy<int>().ThatIsNot(ObjectForEquatableTests.IntegerProperty),
                                 A.Dummy<string>().ThatIsNot(ObjectForEquatableTests.StringProperty),
                                 A.Dummy<IReadOnlyDictionary<string, string>>().ThatIsNot(ObjectForEquatableTests.ReadOnlyDictionaryOfStringString),
                                 ObjectForEquatableTests.ReadOnlyCollectionOfString),
        };

        private static readonly string ObjectThatIsNotTheSameTypeAsObjectForEquatableTests = A.Dummy<string>();

        [Fact]
        public static void ToString___Should_generate_friendly_string_representation_of_object___When_called()
        {
            // Arrange
            var systemUnderTest = A.Dummy<TestModelInner>();

            var expected = Invariant($"CodeGeneration.TestModelInner: BooleanProperty = {systemUnderTest.BooleanProperty}, IntegerProperty = {systemUnderTest.IntegerProperty}, StringProperty = {systemUnderTest.StringProperty}, ReadOnlyDictionaryOfStringString = {systemUnderTest.ReadOnlyDictionaryOfStringString}, ReadOnlyCollectionOfString = {systemUnderTest.ReadOnlyCollectionOfString}.");

            // Act
            var actual = systemUnderTest.ToString();

            // Assert
            actual.Should().Be(expected);
        }

        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Grouping construct for unit test runner.")]
        public static class Constructing
        {
            [Fact]
            public static void TestModelInner___Should_implement_IModel___When_reflecting()
            {
                // Arrange
                var type = typeof(TestModelInner);
                var expectedModelMethods = typeof(IModel<TestModelInner>)
                                          .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                                          .ToList();
                var expectedModelMethodHashes = expectedModelMethods.Select(_ => _.GetSignatureHash());

                // Act
                var actualInterfaces = type.GetAllInterfaces();
                var actualModelMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance).Where(_ => _.DeclaringType == type).ToList();
                var actualModeMethodHashes = actualModelMethods.Select(_ => _.GetSignatureHash());

                // Assert
                actualInterfaces.Should().Contain(typeof(IModel<TestModelInner>));
                actualModeMethodHashes.Should().Contain(expectedModelMethodHashes);
            }

            [Fact]
            public static void Constructor___Should_throw_ArgumentNullException___When_parameter_stringProperty_is_null()
            {
                // Arrange,
                var referenceObject = A.Dummy<TestModelInner>();

                // Act
                var actual = Record.Exception(() => new TestModelInner(
                                 referenceObject.BooleanProperty,
                                 referenceObject.IntegerProperty,
                                 null,
                                 referenceObject.ReadOnlyDictionaryOfStringString,
                                 referenceObject.ReadOnlyCollectionOfString));

                // Assert
                actual.Should().BeOfType<ArgumentNullException>();
                actual.Message.Should().Contain("stringProperty");
            }

            [Fact]
            public static void Constructor___Should_throw_ArgumentException___When_parameter_stringProperty_is_white_space()
            {
                // Arrange,
                var referenceObject = A.Dummy<TestModelInner>();

                // Act
                var actual = Record.Exception(() => new TestModelInner(
                                 referenceObject.BooleanProperty,
                                 referenceObject.IntegerProperty,
                                 Invariant($"  {Environment.NewLine}  "),
                                 referenceObject.ReadOnlyDictionaryOfStringString,
                                 referenceObject.ReadOnlyCollectionOfString));

                // Assert
                actual.Should().BeOfType<ArgumentException>();
                actual.Message.Should().Contain("stringProperty");
                actual.Message.Should().Contain("white space");
            }

            [Fact]
            public static void Constructor___Should_throw_ArgumentNullException___When_parameter_readOnlyDictionaryOfStringString_is_null()
            {
                // Arrange,
                var referenceObject = A.Dummy<TestModelInner>();

                // Act
                var actual = Record.Exception(() => new TestModelInner(
                                 referenceObject.BooleanProperty,
                                 referenceObject.IntegerProperty,
                                 referenceObject.StringProperty,
                                 null,
                                 referenceObject.ReadOnlyCollectionOfString));

                // Assert
                actual.Should().BeOfType<ArgumentNullException>();
                actual.Message.Should().Contain("readOnlyDictionaryOfStringString");
            }

            [Fact]
            public static void Constructor___Should_throw_ArgumentNullException___When_parameter_readOnlyCollectionOfString_is_null()
            {
                // Arrange,
                var referenceObject = A.Dummy<TestModelInner>();

                // Act
                var actual = Record.Exception(() => new TestModelInner(
                                 referenceObject.BooleanProperty,
                                 referenceObject.IntegerProperty,
                                 referenceObject.StringProperty,
                                 referenceObject.ReadOnlyDictionaryOfStringString,
                                 null));

                // Assert
                actual.Should().BeOfType<ArgumentNullException>();
                actual.Message.Should().Contain("readOnlyCollectionOfString");
            }

            [Fact]
            public static void BooleanProperty___Should_return_same_booleanProperty_parameter_passed_to_constructor___When_getting()
            {
                // Arrange,
                var referenceObject = A.Dummy<TestModelInner>();
                var systemUnderTest = new TestModelInner(
                                 referenceObject.BooleanProperty,
                                 referenceObject.IntegerProperty,
                                 referenceObject.StringProperty,
                                 referenceObject.ReadOnlyDictionaryOfStringString,
                                 referenceObject.ReadOnlyCollectionOfString);
                var expected = referenceObject.BooleanProperty;

                // Act
                var actual = systemUnderTest.BooleanProperty;

                // Assert
                actual.Should().Be(expected);
            }

            [Fact]
            public static void IntegerProperty___Should_return_same_integerProperty_parameter_passed_to_constructor___When_getting()
            {
                // Arrange,
                var referenceObject = A.Dummy<TestModelInner>();
                var systemUnderTest = new TestModelInner(
                                 referenceObject.BooleanProperty,
                                 referenceObject.IntegerProperty,
                                 referenceObject.StringProperty,
                                 referenceObject.ReadOnlyDictionaryOfStringString,
                                 referenceObject.ReadOnlyCollectionOfString);
                var expected = referenceObject.IntegerProperty;

                // Act
                var actual = systemUnderTest.IntegerProperty;

                // Assert
                actual.Should().Be(expected);
            }

            [Fact]
            public static void StringProperty___Should_return_same_stringProperty_parameter_passed_to_constructor___When_getting()
            {
                // Arrange,
                var referenceObject = A.Dummy<TestModelInner>();
                var systemUnderTest = new TestModelInner(
                                 referenceObject.BooleanProperty,
                                 referenceObject.IntegerProperty,
                                 referenceObject.StringProperty,
                                 referenceObject.ReadOnlyDictionaryOfStringString,
                                 referenceObject.ReadOnlyCollectionOfString);
                var expected = referenceObject.StringProperty;

                // Act
                var actual = systemUnderTest.StringProperty;

                // Assert
                actual.Should().Be(expected);
            }

            [Fact]
            public static void ReadOnlyDictionaryOfStringString___Should_return_same_readOnlyDictionaryOfStringString_parameter_passed_to_constructor___When_getting()
            {
                // Arrange,
                var referenceObject = A.Dummy<TestModelInner>();
                var systemUnderTest = new TestModelInner(
                                 referenceObject.BooleanProperty,
                                 referenceObject.IntegerProperty,
                                 referenceObject.StringProperty,
                                 referenceObject.ReadOnlyDictionaryOfStringString,
                                 referenceObject.ReadOnlyCollectionOfString);
                var expected = referenceObject.ReadOnlyDictionaryOfStringString;

                // Act
                var actual = systemUnderTest.ReadOnlyDictionaryOfStringString;

                // Assert
                actual.Should().Equal(expected);
            }

            [Fact]
            public static void ReadOnlyCollectionOfString___Should_return_same_readOnlyCollectionOfString_parameter_passed_to_constructor___When_getting()
            {
                // Arrange,
                var referenceObject = A.Dummy<TestModelInner>();
                var systemUnderTest = new TestModelInner(
                                 referenceObject.BooleanProperty,
                                 referenceObject.IntegerProperty,
                                 referenceObject.StringProperty,
                                 referenceObject.ReadOnlyDictionaryOfStringString,
                                 referenceObject.ReadOnlyCollectionOfString);
                var expected = referenceObject.ReadOnlyCollectionOfString;

                // Act
                var actual = systemUnderTest.ReadOnlyCollectionOfString;

                // Assert
                actual.Should().Equal(expected);
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Grouping construct for unit test runner.")]
        public static class Cloning
        {
            [Fact]
            public static void DeepClone___Should_deep_clone_object___When_called()
            {
                // Arrange
                var systemUnderTest = A.Dummy<TestModelInner>();

                // Act
                var actual = systemUnderTest.DeepClone();

                // Assert
                actual.Should().Be(systemUnderTest);
                actual.Should().NotBeSameAs(systemUnderTest);
                actual.ReadOnlyDictionaryOfStringString.Should().NotBeSameAs(systemUnderTest.ReadOnlyDictionaryOfStringString);
                actual.ReadOnlyCollectionOfString.Should().NotBeSameAs(systemUnderTest.ReadOnlyCollectionOfString);
            }

            [Fact]
            public static void DeepCloneWithBooleanProperty___Should_deep_clone_object_and_replace_BooleanProperty_with_the_provided_booleanProperty___When_called()
            {
                // Arrange,
                var systemUnderTest = A.Dummy<TestModelInner>();
                var referenceObject = A.Dummy<TestModelInner>().ThatIsNot(systemUnderTest);

                // Act
                var actual = systemUnderTest.DeepCloneWithBooleanProperty(referenceObject.BooleanProperty);

                // Assert
                actual.BooleanProperty.Should().Be(referenceObject.BooleanProperty);
                actual.IntegerProperty.Should().Be(systemUnderTest.IntegerProperty);
                actual.StringProperty.Should().Be(systemUnderTest.StringProperty);
                actual.ReadOnlyDictionaryOfStringString.Should().Equal(systemUnderTest.ReadOnlyDictionaryOfStringString);
                actual.ReadOnlyDictionaryOfStringString.Should().NotBeSameAs(systemUnderTest.ReadOnlyDictionaryOfStringString);
                actual.ReadOnlyCollectionOfString.Should().Equal(systemUnderTest.ReadOnlyCollectionOfString);
                actual.ReadOnlyCollectionOfString.Should().NotBeSameAs(systemUnderTest.ReadOnlyCollectionOfString);
            }

            [Fact]
            public static void DeepCloneWithIntegerProperty___Should_deep_clone_object_and_replace_IntegerProperty_with_the_provided_integerProperty___When_called()
            {
                // Arrange,
                var systemUnderTest = A.Dummy<TestModelInner>();
                var referenceObject = A.Dummy<TestModelInner>().ThatIsNot(systemUnderTest);

                // Act
                var actual = systemUnderTest.DeepCloneWithIntegerProperty(referenceObject.IntegerProperty);

                // Assert
                actual.BooleanProperty.Should().Be(systemUnderTest.BooleanProperty);
                actual.IntegerProperty.Should().Be(referenceObject.IntegerProperty);
                actual.StringProperty.Should().Be(systemUnderTest.StringProperty);
                actual.ReadOnlyDictionaryOfStringString.Should().Equal(systemUnderTest.ReadOnlyDictionaryOfStringString);
                actual.ReadOnlyDictionaryOfStringString.Should().NotBeSameAs(systemUnderTest.ReadOnlyDictionaryOfStringString);
                actual.ReadOnlyCollectionOfString.Should().Equal(systemUnderTest.ReadOnlyCollectionOfString);
                actual.ReadOnlyCollectionOfString.Should().NotBeSameAs(systemUnderTest.ReadOnlyCollectionOfString);
            }

            [Fact]
            public static void DeepCloneWithStringProperty___Should_deep_clone_object_and_replace_StringProperty_with_the_provided_stringProperty___When_called()
            {
                // Arrange,
                var systemUnderTest = A.Dummy<TestModelInner>();
                var referenceObject = A.Dummy<TestModelInner>().ThatIsNot(systemUnderTest);

                // Act
                var actual = systemUnderTest.DeepCloneWithStringProperty(referenceObject.StringProperty);

                // Assert
                actual.BooleanProperty.Should().Be(systemUnderTest.BooleanProperty);
                actual.IntegerProperty.Should().Be(systemUnderTest.IntegerProperty);
                actual.StringProperty.Should().Be(referenceObject.StringProperty);
                actual.ReadOnlyDictionaryOfStringString.Should().Equal(systemUnderTest.ReadOnlyDictionaryOfStringString);
                actual.ReadOnlyDictionaryOfStringString.Should().NotBeSameAs(systemUnderTest.ReadOnlyDictionaryOfStringString);
                actual.ReadOnlyCollectionOfString.Should().Equal(systemUnderTest.ReadOnlyCollectionOfString);
                actual.ReadOnlyCollectionOfString.Should().NotBeSameAs(systemUnderTest.ReadOnlyCollectionOfString);
            }

            [Fact]
            public static void DeepCloneWithReadOnlyDictionaryOfStringString___Should_deep_clone_object_and_replace_ReadOnlyDictionaryOfStringString_with_the_provided_readOnlyDictionaryOfStringString___When_called()
            {
                // Arrange,
                var systemUnderTest = A.Dummy<TestModelInner>();
                var referenceObject = A.Dummy<TestModelInner>().ThatIsNot(systemUnderTest);

                // Act
                var actual = systemUnderTest.DeepCloneWithReadOnlyDictionaryOfStringString(referenceObject.ReadOnlyDictionaryOfStringString);

                // Assert
                actual.BooleanProperty.Should().Be(systemUnderTest.BooleanProperty);
                actual.IntegerProperty.Should().Be(systemUnderTest.IntegerProperty);
                actual.StringProperty.Should().Be(systemUnderTest.StringProperty);
                actual.ReadOnlyDictionaryOfStringString.Should().Equal(referenceObject.ReadOnlyDictionaryOfStringString);
                actual.ReadOnlyCollectionOfString.Should().Equal(systemUnderTest.ReadOnlyCollectionOfString);
                actual.ReadOnlyCollectionOfString.Should().NotBeSameAs(systemUnderTest.ReadOnlyCollectionOfString);
            }

            [Fact]
            public static void DeepCloneWithReadOnlyCollectionOfString___Should_deep_clone_object_and_replace_ReadOnlyCollectionOfString_with_the_provided_readOnlyCollectionOfString___When_called()
            {
                // Arrange,
                var systemUnderTest = A.Dummy<TestModelInner>();
                var referenceObject = A.Dummy<TestModelInner>().ThatIsNot(systemUnderTest);

                // Act
                var actual = systemUnderTest.DeepCloneWithReadOnlyCollectionOfString(referenceObject.ReadOnlyCollectionOfString);

                // Assert
                actual.BooleanProperty.Should().Be(systemUnderTest.BooleanProperty);
                actual.IntegerProperty.Should().Be(systemUnderTest.IntegerProperty);
                actual.StringProperty.Should().Be(systemUnderTest.StringProperty);
                actual.ReadOnlyDictionaryOfStringString.Should().Equal(systemUnderTest.ReadOnlyDictionaryOfStringString);
                actual.ReadOnlyDictionaryOfStringString.Should().NotBeSameAs(systemUnderTest.ReadOnlyDictionaryOfStringString);
                actual.ReadOnlyCollectionOfString.Should().Equal(referenceObject.ReadOnlyCollectionOfString);
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Grouping construct for unit test runner.")]
        public static class Equality
        {
            [Fact]
            public static void EqualsOperator___Should_return_true___When_both_sides_of_operator_are_null()
            {
                // Arrange
                TestModelInner systemUnderTest1 = null;
                TestModelInner systemUnderTest2 = null;

                // Act
                var result = systemUnderTest1 == systemUnderTest2;

                // Assert
                result.Should().BeTrue();
            }

            [Fact]
            public static void EqualsOperator___Should_return_false___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
            {
                // Arrange
                TestModelInner systemUnderTest = null;

                // Act
                var result1 = systemUnderTest == ObjectForEquatableTests;
                var result2 = ObjectForEquatableTests == systemUnderTest;

                // Assert
                result1.Should().BeFalse();
                result2.Should().BeFalse();
            }

            [Fact]
            public static void EqualsOperator___Should_return_true___When_same_object_is_on_both_sides_of_operator()
            {
                // Arrange, Act
#pragma warning disable CS1718 // Comparison made to same variable
                var result = ObjectForEquatableTests == ObjectForEquatableTests;
#pragma warning restore CS1718 // Comparison made to same variable

                // Assert
                result.Should().BeTrue();
            }

            [Fact]
            public static void EqualsOperator___Should_return_false___When_objects_being_compared_have_different_property_values()
            {
                // Arrange, Act
                var actualCheckReferenceAgainstUnequalSet = ObjectsThatAreNotEqualToObjectForEquatableTests.Select(_ => ObjectForEquatableTests == _).ToList();
                var actualCheckAgainstOthersInUnequalSet = ObjectsThatAreNotEqualToObjectForEquatableTests.GetCombinations(2, 2).Select(_ => _.First() == _.Last()).ToList();

                // Assert
                actualCheckReferenceAgainstUnequalSet.ForEach(_ => _.Should().BeFalse());
                actualCheckAgainstOthersInUnequalSet.ForEach(_ => _.Should().BeFalse());
            }

            [Fact]
            public static void EqualsOperator___Should_return_true___When_objects_being_compared_have_same_property_values()
            {
                // Arrange, Act
                var result = ObjectForEquatableTests == ObjectThatIsEqualToButNotTheSameAsObjectForEquatableTests;

                // Assert
                result.Should().BeTrue();
            }

            [Fact]
            public static void NotEqualsOperator___Should_return_false___When_both_sides_of_operator_are_null()
            {
                // Arrange
                TestModelInner systemUnderTest1 = null;
                TestModelInner systemUnderTest2 = null;

                // Act
                var result = systemUnderTest1 != systemUnderTest2;

                // Assert
                result.Should().BeFalse();
            }

            [Fact]
            public static void NotEqualsOperator___Should_return_true___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
            {
                // Arrange
                TestModelInner systemUnderTest = null;

                // Act
                var result1 = systemUnderTest != ObjectForEquatableTests;
                var result2 = ObjectForEquatableTests != systemUnderTest;

                // Assert
                result1.Should().BeTrue();
                result2.Should().BeTrue();
            }

            [Fact]
            public static void NotEqualsOperator___Should_return_false___When_same_object_is_on_both_sides_of_operator()
            {
                // Arrange, Act
#pragma warning disable CS1718 // Comparison made to same variable
                var result = ObjectForEquatableTests != ObjectForEquatableTests;
#pragma warning restore CS1718 // Comparison made to same variable

                // Assert
                result.Should().BeFalse();
            }

            [Fact]
            public static void NotEqualsOperator___Should_return_true___When_objects_being_compared_have_different_property_values()
            {
                // Arrange, Act
                var actualCheckReferenceAgainstUnequalSet = ObjectsThatAreNotEqualToObjectForEquatableTests.Select(_ => ObjectForEquatableTests != _).ToList();
                var actualCheckAgainstOthersInUnequalSet = ObjectsThatAreNotEqualToObjectForEquatableTests.GetCombinations(2, 2).Select(_ => _.First() != _.Last()).ToList();

                // Assert
                actualCheckReferenceAgainstUnequalSet.ForEach(_ => _.Should().BeTrue());
                actualCheckAgainstOthersInUnequalSet.ForEach(_ => _.Should().BeTrue());
            }

            [Fact]
            public static void NotEqualsOperator___Should_return_false___When_objects_being_compared_have_same_property_values()
            {
                // Arrange, Act
                var result = ObjectForEquatableTests != ObjectThatIsEqualToButNotTheSameAsObjectForEquatableTests;

                // Assert
                result.Should().BeFalse();
            }

            [Fact]
            public static void Equals_with_TestModelInner___Should_return_false___When_parameter_other_is_null()
            {
                // Arrange
                TestModelInner systemUnderTest = null;

                // Act
                var result = ObjectForEquatableTests.Equals(systemUnderTest);

                // Assert
                result.Should().BeFalse();
            }

            [Fact]
            public static void Equals_with_TestModelInner___Should_return_true___When_parameter_other_is_same_object()
            {
                // Arrange, Act
                var result = ObjectForEquatableTests.Equals(ObjectForEquatableTests);

                // Assert
                result.Should().BeTrue();
            }

            [Fact]
            public static void Equals_with_TestModelInner___Should_return_false___When_objects_being_compared_have_different_property_values()
            {
                // Arrange, Act
                var actualCheckReferenceAgainstUnequalSet = ObjectsThatAreNotEqualToObjectForEquatableTests.Select(_ => ObjectForEquatableTests.Equals(_)).ToList();
                var actualCheckAgainstOthersInUnequalSet = ObjectsThatAreNotEqualToObjectForEquatableTests.GetCombinations(2, 2).Select(_ => _.First().Equals(_.Last())).ToList();

                // Assert
                actualCheckReferenceAgainstUnequalSet.ForEach(_ => _.Should().BeFalse());
                actualCheckAgainstOthersInUnequalSet.ForEach(_ => _.Should().BeFalse());
            }

            [Fact]
            public static void Equals_with_TestModelInner___Should_return_true___When_objects_being_compared_have_same_property_values()
            {
                // Arrange, Act
                var result = ObjectForEquatableTests.Equals(ObjectThatIsEqualToButNotTheSameAsObjectForEquatableTests);

                // Assert
                result.Should().BeTrue();
            }

            [Fact]
            public static void Equals_with_Object___Should_return_false___When_parameter_other_is_null()
            {
                // Arrange, Act
                var result = ObjectForEquatableTests.Equals(null);

                // Assert
                result.Should().BeFalse();
            }

            [Fact]
            public static void Equals_with_Object___Should_return_false___When_parameter_other_is_not_of_the_same_type()
            {
                // Arrange, Act
                var result = ObjectForEquatableTests.Equals((object)ObjectThatIsNotTheSameTypeAsObjectForEquatableTests);

                // Assert
                result.Should().BeFalse();
            }

            [Fact]
            public static void Equals_with_Object___Should_return_true___When_parameter_other_is_same_object()
            {
                // Arrange, Act
                var result = ObjectForEquatableTests.Equals((object)ObjectForEquatableTests);

                // Assert
                result.Should().BeTrue();
            }

            [Fact]
            public static void Equals_with_Object___Should_return_false___When_objects_being_compared_have_different_property_values()
            {
                // Arrange, Act
                var actualCheckReferenceAgainstUnequalSet = ObjectsThatAreNotEqualToObjectForEquatableTests.Select(_ => ObjectForEquatableTests.Equals((object)_)).ToList();
                var actualCheckAgainstOthersInUnequalSet = ObjectsThatAreNotEqualToObjectForEquatableTests.GetCombinations(2, 2).Select(_ => _.First().Equals((object)_.Last())).ToList();

                // Assert
                actualCheckReferenceAgainstUnequalSet.ForEach(_ => _.Should().BeFalse());
                actualCheckAgainstOthersInUnequalSet.ForEach(_ => _.Should().BeFalse());
            }

            [Fact]
            public static void Equals_with_Object___Should_return_true___When_objects_being_compared_have_same_property_values()
            {
                // Arrange, Act
                var result = ObjectForEquatableTests.Equals((object)ObjectThatIsEqualToButNotTheSameAsObjectForEquatableTests);

                // Assert
                result.Should().BeTrue();
            }

            [Fact]
            public static void GetHashCode___Should_not_be_equal_for_two_objects___When_objects_have_different_property_values()
            {
                // Arrange, Act
                var actualHashCodeOfReference = ObjectForEquatableTests.GetHashCode();
                var actualHashCodesInNotEqualSet = ObjectsThatAreNotEqualToObjectForEquatableTests.Select(_ => _.GetHashCode()).ToList();
                var actualEqualityCheckOfHashCodesAgainstOthersInNotEqualSet = ObjectsThatAreNotEqualToObjectForEquatableTests.GetCombinations(2, 2).Select(_ => _.First().GetHashCode() == _.Last().GetHashCode()).ToList();

                // Assert
                actualHashCodesInNotEqualSet.Should().NotContain(actualHashCodeOfReference);
                actualEqualityCheckOfHashCodesAgainstOthersInNotEqualSet.ForEach(_ => _.Should().BeFalse());
            }

            [Fact]
            public static void GetHashCode___Should_be_equal_for_two_objects___When_objects_have_the_same_property_values()
            {
                // Arrange, Act
                var hash1 = ObjectForEquatableTests.GetHashCode();
                var hash2 = ObjectThatIsEqualToButNotTheSameAsObjectForEquatableTests.GetHashCode();

                // Assert
                hash1.Should().Be(hash2);
            }
        }
    }
}