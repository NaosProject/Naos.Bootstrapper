// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestModelOuterTest.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper.Test.CodeGeneration.TestModelOuterTests
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

    public partial class TestModelOuterTest
    {
        private readonly ITestOutputHelper testOutputHelper;

        public TestModelOuterTest(
            ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void GenerateModel()
        {
            var results = CodeGenerator.GenerateForModel<TestModelOuter>(CodeGenerator.GenerateFor.ModelImplementationPartialClass);
            this.testOutputHelper.WriteLine(results);
        }

        [Fact]
        public void GenerateTests()
        {
            var results = CodeGenerator.GenerateForModel<TestModelOuter>(
                CodeGenerator.GenerateFor.ModelImplementationTestsPartialClassWithoutSerialization);
            this.testOutputHelper.WriteLine(results);
        }
    }

    public partial class TestModelOuterTest
    {
        private static readonly TestModelOuter ObjectForEquatableTests = A.Dummy<TestModelOuter>();

        private static readonly TestModelOuter ObjectThatIsEqualToButNotTheSameAsObjectForEquatableTests =
            new TestModelOuter(
                                 ObjectForEquatableTests.TestModelInnerProperty,
                                 ObjectForEquatableTests.ReadOnlyDictionaryOfTestModelInnerTestModelInner,
                                 ObjectForEquatableTests.ReadOnlyCollectionOfTestModelInner);

        private static readonly TestModelOuter[] ObjectsThatAreNotEqualToObjectForEquatableTests =
        {
            new TestModelOuter(
                                 ObjectForEquatableTests.TestModelInnerProperty,
                                 A.Dummy<IReadOnlyDictionary<TestModelInner, TestModelInner>>().ThatIsNot(ObjectForEquatableTests.ReadOnlyDictionaryOfTestModelInnerTestModelInner),
                                 A.Dummy<IReadOnlyCollection<TestModelInner>>().ThatIsNot(ObjectForEquatableTests.ReadOnlyCollectionOfTestModelInner)),
            new TestModelOuter(
                                 A.Dummy<TestModelInner>().ThatIsNot(ObjectForEquatableTests.TestModelInnerProperty),
                                 ObjectForEquatableTests.ReadOnlyDictionaryOfTestModelInnerTestModelInner,
                                 A.Dummy<IReadOnlyCollection<TestModelInner>>().ThatIsNot(ObjectForEquatableTests.ReadOnlyCollectionOfTestModelInner)),
            new TestModelOuter(
                                 A.Dummy<TestModelInner>().ThatIsNot(ObjectForEquatableTests.TestModelInnerProperty),
                                 A.Dummy<IReadOnlyDictionary<TestModelInner, TestModelInner>>().ThatIsNot(ObjectForEquatableTests.ReadOnlyDictionaryOfTestModelInnerTestModelInner),
                                 ObjectForEquatableTests.ReadOnlyCollectionOfTestModelInner),
        };

        private static readonly string ObjectThatIsNotTheSameTypeAsObjectForEquatableTests = A.Dummy<string>();

        [Fact]
        public static void ToString___Should_generate_friendly_string_representation_of_object___When_called()
        {
            // Arrange
            var systemUnderTest = A.Dummy<TestModelOuter>();

            var expected = Invariant($"CodeGeneration.TestModelOuter: TestModelInnerProperty = {systemUnderTest.TestModelInnerProperty}, ReadOnlyDictionaryOfTestModelInnerTestModelInner = {systemUnderTest.ReadOnlyDictionaryOfTestModelInnerTestModelInner}, ReadOnlyCollectionOfTestModelInner = {systemUnderTest.ReadOnlyCollectionOfTestModelInner}.");

            // Act
            var actual = systemUnderTest.ToString();

            // Assert
            actual.Should().Be(expected);
        }

        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Grouping construct for unit test runner.")]
        public static class Constructing
        {
            [Fact]
            public static void TestModelOuter___Should_implement_IModel___When_reflecting()
            {
                // Arrange
                var type = typeof(TestModelOuter);
                var expectedModelMethods = typeof(IModel<TestModelOuter>)
                                          .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                                          .ToList();
                var expectedModelMethodHashes = expectedModelMethods.Select(_ => _.GetSignatureHash());

                // Act
                var actualInterfaces = type.GetAllInterfaces();
                var actualModelMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance).Where(_ => _.DeclaringType == type).ToList();
                var actualModeMethodHashes = actualModelMethods.Select(_ => _.GetSignatureHash());

                // Assert
                actualInterfaces.Should().Contain(typeof(IModel<TestModelOuter>));
                actualModeMethodHashes.Should().Contain(expectedModelMethodHashes);
            }

            [Fact]
            public static void Constructor___Should_throw_ArgumentNullException___When_parameter_testModelInnerProperty_is_null()
            {
                // Arrange,
                var referenceObject = A.Dummy<TestModelOuter>();

                // Act
                var actual = Record.Exception(() => new TestModelOuter(
                                 null,
                                 referenceObject.ReadOnlyDictionaryOfTestModelInnerTestModelInner,
                                 referenceObject.ReadOnlyCollectionOfTestModelInner));

                // Assert
                actual.Should().BeOfType<ArgumentNullException>();
                actual.Message.Should().Contain("testModelInnerProperty");
            }

            [Fact]
            public static void Constructor___Should_throw_ArgumentNullException___When_parameter_readOnlyDictionaryOfTestModelInnerTestModelInner_is_null()
            {
                // Arrange,
                var referenceObject = A.Dummy<TestModelOuter>();

                // Act
                var actual = Record.Exception(() => new TestModelOuter(
                                 referenceObject.TestModelInnerProperty,
                                 null,
                                 referenceObject.ReadOnlyCollectionOfTestModelInner));

                // Assert
                actual.Should().BeOfType<ArgumentNullException>();
                actual.Message.Should().Contain("readOnlyDictionaryOfTestModelInnerTestModelInner");
            }

            [Fact]
            public static void Constructor___Should_throw_ArgumentNullException___When_parameter_readOnlyCollectionOfTestModelInner_is_null()
            {
                // Arrange,
                var referenceObject = A.Dummy<TestModelOuter>();

                // Act
                var actual = Record.Exception(() => new TestModelOuter(
                                 referenceObject.TestModelInnerProperty,
                                 referenceObject.ReadOnlyDictionaryOfTestModelInnerTestModelInner,
                                 null));

                // Assert
                actual.Should().BeOfType<ArgumentNullException>();
                actual.Message.Should().Contain("readOnlyCollectionOfTestModelInner");
            }

            [Fact]
            public static void TestModelInnerProperty___Should_return_same_testModelInnerProperty_parameter_passed_to_constructor___When_getting()
            {
                // Arrange,
                var referenceObject = A.Dummy<TestModelOuter>();
                var systemUnderTest = new TestModelOuter(
                                 referenceObject.TestModelInnerProperty,
                                 referenceObject.ReadOnlyDictionaryOfTestModelInnerTestModelInner,
                                 referenceObject.ReadOnlyCollectionOfTestModelInner);
                var expected = referenceObject.TestModelInnerProperty;

                // Act
                var actual = systemUnderTest.TestModelInnerProperty;

                // Assert
                actual.Should().Be(expected);
            }

            [Fact]
            public static void ReadOnlyDictionaryOfTestModelInnerTestModelInner___Should_return_same_readOnlyDictionaryOfTestModelInnerTestModelInner_parameter_passed_to_constructor___When_getting()
            {
                // Arrange,
                var referenceObject = A.Dummy<TestModelOuter>();
                var systemUnderTest = new TestModelOuter(
                                 referenceObject.TestModelInnerProperty,
                                 referenceObject.ReadOnlyDictionaryOfTestModelInnerTestModelInner,
                                 referenceObject.ReadOnlyCollectionOfTestModelInner);
                var expected = referenceObject.ReadOnlyDictionaryOfTestModelInnerTestModelInner;

                // Act
                var actual = systemUnderTest.ReadOnlyDictionaryOfTestModelInnerTestModelInner;

                // Assert
                actual.Should().Equal(expected);
            }

            [Fact]
            public static void ReadOnlyCollectionOfTestModelInner___Should_return_same_readOnlyCollectionOfTestModelInner_parameter_passed_to_constructor___When_getting()
            {
                // Arrange,
                var referenceObject = A.Dummy<TestModelOuter>();
                var systemUnderTest = new TestModelOuter(
                                 referenceObject.TestModelInnerProperty,
                                 referenceObject.ReadOnlyDictionaryOfTestModelInnerTestModelInner,
                                 referenceObject.ReadOnlyCollectionOfTestModelInner);
                var expected = referenceObject.ReadOnlyCollectionOfTestModelInner;

                // Act
                var actual = systemUnderTest.ReadOnlyCollectionOfTestModelInner;

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
                var systemUnderTest = A.Dummy<TestModelOuter>();

                // Act
                var actual = systemUnderTest.DeepClone();

                // Assert
                actual.Should().Be(systemUnderTest);
                actual.Should().NotBeSameAs(systemUnderTest);
                actual.TestModelInnerProperty.Should().NotBeSameAs(systemUnderTest.TestModelInnerProperty);
                actual.ReadOnlyDictionaryOfTestModelInnerTestModelInner.Should().NotBeSameAs(systemUnderTest.ReadOnlyDictionaryOfTestModelInnerTestModelInner);
                actual.ReadOnlyCollectionOfTestModelInner.Should().NotBeSameAs(systemUnderTest.ReadOnlyCollectionOfTestModelInner);
            }

            [Fact]
            public static void DeepCloneWithTestModelInnerProperty___Should_deep_clone_object_and_replace_TestModelInnerProperty_with_the_provided_testModelInnerProperty___When_called()
            {
                // Arrange,
                var systemUnderTest = A.Dummy<TestModelOuter>();
                var referenceObject = A.Dummy<TestModelOuter>().ThatIsNot(systemUnderTest);

                // Act
                var actual = systemUnderTest.DeepCloneWithTestModelInnerProperty(referenceObject.TestModelInnerProperty);

                // Assert
                actual.TestModelInnerProperty.Should().Be(referenceObject.TestModelInnerProperty);
                actual.ReadOnlyDictionaryOfTestModelInnerTestModelInner.Should().Equal(systemUnderTest.ReadOnlyDictionaryOfTestModelInnerTestModelInner);
                actual.ReadOnlyDictionaryOfTestModelInnerTestModelInner.Should().NotBeSameAs(systemUnderTest.ReadOnlyDictionaryOfTestModelInnerTestModelInner);
                actual.ReadOnlyCollectionOfTestModelInner.Should().Equal(systemUnderTest.ReadOnlyCollectionOfTestModelInner);
                actual.ReadOnlyCollectionOfTestModelInner.Should().NotBeSameAs(systemUnderTest.ReadOnlyCollectionOfTestModelInner);
            }

            [Fact]
            public static void DeepCloneWithReadOnlyDictionaryOfTestModelInnerTestModelInner___Should_deep_clone_object_and_replace_ReadOnlyDictionaryOfTestModelInnerTestModelInner_with_the_provided_readOnlyDictionaryOfTestModelInnerTestModelInner___When_called()
            {
                // Arrange,
                var systemUnderTest = A.Dummy<TestModelOuter>();
                var referenceObject = A.Dummy<TestModelOuter>().ThatIsNot(systemUnderTest);

                // Act
                var actual = systemUnderTest.DeepCloneWithReadOnlyDictionaryOfTestModelInnerTestModelInner(referenceObject.ReadOnlyDictionaryOfTestModelInnerTestModelInner);

                // Assert
                actual.TestModelInnerProperty.Should().Be(systemUnderTest.TestModelInnerProperty);
                actual.TestModelInnerProperty.Should().NotBeSameAs(systemUnderTest.TestModelInnerProperty);
                actual.ReadOnlyDictionaryOfTestModelInnerTestModelInner.Should().Equal(referenceObject.ReadOnlyDictionaryOfTestModelInnerTestModelInner);
                actual.ReadOnlyCollectionOfTestModelInner.Should().Equal(systemUnderTest.ReadOnlyCollectionOfTestModelInner);
                actual.ReadOnlyCollectionOfTestModelInner.Should().NotBeSameAs(systemUnderTest.ReadOnlyCollectionOfTestModelInner);
            }

            [Fact]
            public static void DeepCloneWithReadOnlyCollectionOfTestModelInner___Should_deep_clone_object_and_replace_ReadOnlyCollectionOfTestModelInner_with_the_provided_readOnlyCollectionOfTestModelInner___When_called()
            {
                // Arrange,
                var systemUnderTest = A.Dummy<TestModelOuter>();
                var referenceObject = A.Dummy<TestModelOuter>().ThatIsNot(systemUnderTest);

                // Act
                var actual = systemUnderTest.DeepCloneWithReadOnlyCollectionOfTestModelInner(referenceObject.ReadOnlyCollectionOfTestModelInner);

                // Assert
                actual.TestModelInnerProperty.Should().Be(systemUnderTest.TestModelInnerProperty);
                actual.TestModelInnerProperty.Should().NotBeSameAs(systemUnderTest.TestModelInnerProperty);
                actual.ReadOnlyDictionaryOfTestModelInnerTestModelInner.Should().Equal(systemUnderTest.ReadOnlyDictionaryOfTestModelInnerTestModelInner);
                actual.ReadOnlyDictionaryOfTestModelInnerTestModelInner.Should().NotBeSameAs(systemUnderTest.ReadOnlyDictionaryOfTestModelInnerTestModelInner);
                actual.ReadOnlyCollectionOfTestModelInner.Should().Equal(referenceObject.ReadOnlyCollectionOfTestModelInner);
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Grouping construct for unit test runner.")]
        public static class Equality
        {
            [Fact]
            public static void EqualsOperator___Should_return_true___When_both_sides_of_operator_are_null()
            {
                // Arrange
                TestModelOuter systemUnderTest1 = null;
                TestModelOuter systemUnderTest2 = null;

                // Act
                var result = systemUnderTest1 == systemUnderTest2;

                // Assert
                result.Should().BeTrue();
            }

            [Fact]
            public static void EqualsOperator___Should_return_false___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
            {
                // Arrange
                TestModelOuter systemUnderTest = null;

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
                TestModelOuter systemUnderTest1 = null;
                TestModelOuter systemUnderTest2 = null;

                // Act
                var result = systemUnderTest1 != systemUnderTest2;

                // Assert
                result.Should().BeFalse();
            }

            [Fact]
            public static void NotEqualsOperator___Should_return_true___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
            {
                // Arrange
                TestModelOuter systemUnderTest = null;

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
            public static void Equals_with_TestModelOuter___Should_return_false___When_parameter_other_is_null()
            {
                // Arrange
                TestModelOuter systemUnderTest = null;

                // Act
                var result = ObjectForEquatableTests.Equals(systemUnderTest);

                // Assert
                result.Should().BeFalse();
            }

            [Fact]
            public static void Equals_with_TestModelOuter___Should_return_true___When_parameter_other_is_same_object()
            {
                // Arrange, Act
                var result = ObjectForEquatableTests.Equals(ObjectForEquatableTests);

                // Assert
                result.Should().BeTrue();
            }

            [Fact]
            public static void Equals_with_TestModelOuter___Should_return_false___When_objects_being_compared_have_different_property_values()
            {
                // Arrange, Act
                var actualCheckReferenceAgainstUnequalSet = ObjectsThatAreNotEqualToObjectForEquatableTests.Select(_ => ObjectForEquatableTests.Equals(_)).ToList();
                var actualCheckAgainstOthersInUnequalSet = ObjectsThatAreNotEqualToObjectForEquatableTests.GetCombinations(2, 2).Select(_ => _.First().Equals(_.Last())).ToList();

                // Assert
                actualCheckReferenceAgainstUnequalSet.ForEach(_ => _.Should().BeFalse());
                actualCheckAgainstOthersInUnequalSet.ForEach(_ => _.Should().BeFalse());
            }

            [Fact]
            public static void Equals_with_TestModelOuter___Should_return_true___When_objects_being_compared_have_same_property_values()
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