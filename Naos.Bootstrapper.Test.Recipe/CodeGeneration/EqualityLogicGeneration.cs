// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EqualityLogicGeneration.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper.Test.CodeGeneration
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using OBeautifulCode.Validation.Recipes;
    using static System.FormattableString;

    public static class EqualityLogicGeneration
    {
        private const string TypeNameToken = "<<<TypeNameHere>>>";
        private const string EqualityToken = "<<<EqualityLogicHere>>>";
        private const string NewObjectForEquatableToken = "<<<NewObjectLogicForEquatableHere>>>";
        private const string UnequalObjectsToken = "<<<UnequalObjectsCreationHere>>>";

        private const string EqualityMethodsCodeTemplate = @"    /// <summary>
        /// Determines whether two objects of type <see cref=""" + TypeNameToken + @"""/> are equal.
        /// </summary>
        /// <param name=""left"">The object to the left of the operator.</param>
        /// <param name=""right"">The object to the right of the operator.</param>
        /// <returns>True if the two items are equal; otherwise false.</returns>
        public static bool operator ==(" + TypeNameToken + @" left, " + TypeNameToken + @" right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            {
                return false;
            }

            var result = " + EqualityToken + @";

            return result;
        }

        /// <summary>
        /// Determines whether two objects of type <see cref=""" + TypeNameToken + @"""/> are not equal.
        /// </summary>
        /// <param name=""left"">The object to the left of the operator.</param>
        /// <param name=""right"">The object to the right of the operator.</param>
        /// <returns>True if the two items not equal; otherwise false.</returns>
        public static bool operator !=(" + TypeNameToken + @" left, " + TypeNameToken + @" right) => !(left == right);

        /// <inheritdoc />
        public bool Equals(" + TypeNameToken + @" other) => this == other;

        /// <inheritdoc />
        public override bool Equals(object obj) => this == (obj as " + TypeNameToken + @");";

        private const string EqualityTestFieldsCodeTemplate = @"    private static readonly " + TypeNameToken + @" ObjectForEquatableTests = A.Dummy<" + TypeNameToken + @">();

        private static readonly " + TypeNameToken + @" ObjectThatIsEqualToButNotTheSameAsObjectForEquatableTests =
            " + NewObjectForEquatableToken + @";

        private static readonly " + TypeNameToken + @"[] ObjectsThatAreNotEqualToObjectForEquatableTests =
        {
            " + UnequalObjectsToken + @"
        };

        private static readonly string ObjectThatIsNotTheSameTypeAsObjectForEquatableTests = A.Dummy<string>();";

        private const string EqualityTestMethodsCodeTemplate = @"
        [SuppressMessage(""Microsoft.Design"", ""CA1034:NestedTypesShouldNotBeVisible"", Justification = ""Grouping construct for unit test runner."")]
        public static class Equality
        {
            [Fact]
            public static void EqualsOperator___Should_return_true___When_both_sides_of_operator_are_null()
            {
                // Arrange
                " + TypeNameToken + @" systemUnderTest1 = null;
                " + TypeNameToken + @" systemUnderTest2 = null;

                // Act
                var result = systemUnderTest1 == systemUnderTest2;

                // Assert
                result.Should().BeTrue();
            }

            [Fact]
            public static void EqualsOperator___Should_return_false___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
            {
                // Arrange
                " + TypeNameToken + @" systemUnderTest = null;

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
                var actualCheckAgainstOthersInUnequalSet = ObjectsThatAreNotEqualToObjectForEquatableTests.GetCombinations(2, 2).Select( _=>_ .First() == _.Last()).ToList();

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
                " + TypeNameToken + @" systemUnderTest1 = null;
                " + TypeNameToken + @" systemUnderTest2 = null;

                // Act
                var result = systemUnderTest1 != systemUnderTest2;

                // Assert
                result.Should().BeFalse();
            }

            [Fact]
            public static void NotEqualsOperator___Should_return_true___When_one_side_of_operator_is_null_and_the_other_side_is_not_null()
            {
                // Arrange
                " + TypeNameToken + @" systemUnderTest = null;

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
                var actualCheckAgainstOthersInUnequalSet = ObjectsThatAreNotEqualToObjectForEquatableTests.GetCombinations(2, 2).Select( _=>_ .First() != _.Last()).ToList();

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
            public static void Equals_with_" + TypeNameToken + @"___Should_return_false___When_parameter_other_is_null()
            {
                // Arrange
                " + TypeNameToken + @" systemUnderTest = null;

                // Act
                var result = ObjectForEquatableTests.Equals(systemUnderTest);

                // Assert
                result.Should().BeFalse();
            }

            [Fact]
            public static void Equals_with_" + TypeNameToken + @"___Should_return_true___When_parameter_other_is_same_object()
            {
                // Arrange, Act
                var result = ObjectForEquatableTests.Equals(ObjectForEquatableTests);

                // Assert
                result.Should().BeTrue();
            }

            [Fact]
            public static void Equals_with_" + TypeNameToken + @"___Should_return_false___When_objects_being_compared_have_different_property_values()
            {
                // Arrange, Act
                var actualCheckReferenceAgainstUnequalSet = ObjectsThatAreNotEqualToObjectForEquatableTests.Select(_ => ObjectForEquatableTests.Equals(_)).ToList();
                var actualCheckAgainstOthersInUnequalSet = ObjectsThatAreNotEqualToObjectForEquatableTests.GetCombinations(2, 2).Select( _=> _.First().Equals(_.Last())).ToList();

                // Assert
                actualCheckReferenceAgainstUnequalSet.ForEach(_ => _.Should().BeFalse());
                actualCheckAgainstOthersInUnequalSet.ForEach(_ => _.Should().BeFalse());
            }

            [Fact]
            public static void Equals_with_" + TypeNameToken + @"___Should_return_true___When_objects_being_compared_have_same_property_values()
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
                var actualCheckAgainstOthersInUnequalSet = ObjectsThatAreNotEqualToObjectForEquatableTests.GetCombinations(2, 2).Select( _=>_ .First().Equals((object)_.Last())).ToList();

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
        }";

        public static string GenerateEqualityMethods(
            this Type type)
        {
            var properties = type.GetPropertiesOfConcernFromType();
            var equalityLines = properties.Select(_ => _.GenerateEqualityLogicCodeForProperty()).ToList();
            var equalityToken = string.Join(Environment.NewLine + "                      && ", equalityLines);
            var result = EqualityMethodsCodeTemplate.Replace(TypeNameToken, type.TreatedTypeName())
                                                    .Replace(EqualityToken, equalityToken);

            return result;
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Want Type to be the type.")]
        public static string GenerateEqualityTestMethods(
            this Type type)
        {
            var result = EqualityTestMethodsCodeTemplate.Replace(TypeNameToken, type.TreatedTypeName());

            return result;
        }

        public static string GenerateEqualityTestFields(
            this Type type)
        {
            var properties = type.GetPropertiesOfConcernFromType();
            var unequalSet = new List<string>();
            foreach (var property in properties)
            {
                var propertyNameToSourceCodeMap = properties.ToDictionary(
                    k => k.Name,
                    v =>
                    {
                        var referenceObject = "ObjectForEquatableTests." + v.Name;
                        return v.Name == property.Name ? referenceObject : v.PropertyType.GenerateDummyConstructionCodeForType(referenceObject);
                    });

                var code = type.GenerateNewLogicCodeForTypeWithSources(propertyNameToSourceCodeMap);
                unequalSet.Add(code);
            }

            var unequalObjectsToken = string.Join("," + Environment.NewLine + "            ", unequalSet) + ",";

            var propertyNameToSourceCodeMapForNewForEquatable = properties.ToDictionary(k => k.Name, v =>
            {
                var referenceObject = "ObjectForEquatableTests." + v.Name;
                return referenceObject;
            });

            var newObjectFromEquatableToken = type.GenerateNewLogicCodeForTypeWithSources(propertyNameToSourceCodeMapForNewForEquatable);

            var result = EqualityTestFieldsCodeTemplate.Replace(TypeNameToken, type.TreatedTypeName())
                                                       .Replace(UnequalObjectsToken,        unequalObjectsToken)
                                                       .Replace(NewObjectForEquatableToken, newObjectFromEquatableToken);

            return result;
        }

        private static string GenerateEqualityLogicCodeForProperty(
            this PropertyInfo propertyInfo)
        {
            propertyInfo.Named(nameof(propertyInfo)).Must().NotBeNull();

            if (propertyInfo.PropertyType.IsAssignableToAnyDictionary())
            {
                return Invariant($"left.{propertyInfo.Name}.DictionaryEqual(right.{propertyInfo.Name})");
            }
            else if (propertyInfo.PropertyType.IsAssignableToAnyCollection())
            {
                return Invariant($"left.{propertyInfo.Name}.SequenceEqualHandlingNulls(right.{propertyInfo.Name})");
            }
            else if (propertyInfo.PropertyType == typeof(string))
            {
                return Invariant($"left.{propertyInfo.Name}.Equals(right.{propertyInfo.Name}, StringComparison.Ordinal)");
            }
            else
            {
                return Invariant($"left.{propertyInfo.Name} == right.{propertyInfo.Name}");
            }
        }
    }
}