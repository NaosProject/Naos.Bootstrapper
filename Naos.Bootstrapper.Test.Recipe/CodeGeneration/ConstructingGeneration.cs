// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstructingGeneration.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper.Test.CodeGeneration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OBeautifulCode.Collection.Recipes;
    using OBeautifulCode.Validation.Recipes;
    using static System.FormattableString;

    public static class ConstructingGeneration
    {
        private const string TypeNameToken = "<<<TypeNameHere>>>";
        private const string ConstructorParameterToken = "<<<ConstructorParameterUnderTest>>>";
        private const string ConstructorTestInflationToken = "<<<ConstructorTestMethodsInflatedGoesHere>>>";
        private const string NewObjectForArgumentNullTestToken = "<<<NewObjectWithOneArgumentNullHere>>>";
        private const string NewObjectForArgumentWhiteSpaceTestToken = "<<<NewObjectWithOneArgumentWhiteSpaceHere>>>";
        private const string PropertyNameToken = "<<<PropertyNameHere>>>";
        private const string AssertPropertyGetterToken = "<<<AssertPropertyGetterHere>>>";
        private const string NewObjectForGetterTestToken = "<<<NewObjectWithOneArgumentFromOtherHere>>>";

        private const string ConstructingTestMethodsCodeTemplate = @"
        [SuppressMessage(""Microsoft.Design"", ""CA1034:NestedTypesShouldNotBeVisible"", Justification = ""Grouping construct for unit test runner."")]
        public static class Constructing
        {
            [Fact]
            public static void " + TypeNameToken + @"___Should_implement_IModel___When_reflecting()
            {
                // Arrange
                var type = typeof(" + TypeNameToken + @");
                var expectedModelMethods = typeof(IModel<" + TypeNameToken + @">)
                                          .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                                          .ToList();
                var expectedModelMethodHashes = expectedModelMethods.Select(_ => _.GetSignatureHash());

                // Act
                var actualInterfaces = type.GetAllInterfaces();
                var actualModelMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance).Where(_ => _.DeclaringType == type).ToList();
                var actualModeMethodHashes = actualModelMethods.Select(_ => _.GetSignatureHash());

                // Assert
                actualInterfaces.Should().Contain(typeof(IModel<" + TypeNameToken + @">));
                actualModeMethodHashes.Should().Contain(expectedModelMethodHashes);
            }" + ConstructorTestInflationToken + @"
        }";

        private const string ConstructorTestMethodForArgumentCodeTemplate = @"
            [Fact]
            public static void Constructor___Should_throw_ArgumentNullException___When_parameter_" + ConstructorParameterToken + @"_is_null()
            {
                // Arrange,
                var referenceObject = A.Dummy<" + TypeNameToken + @">();

                // Act
                var actual = Record.Exception(() => " + NewObjectForArgumentNullTestToken + @");

                // Assert
                actual.Should().BeOfType<ArgumentNullException>();
                actual.Message.Should().Contain(""" + ConstructorParameterToken + @""");
            }";

        private const string ConstructorTestMethodForStringArgumentCodeTemplate = @"
            [Fact]
            public static void Constructor___Should_throw_ArgumentException___When_parameter_" + ConstructorParameterToken + @"_is_white_space()
            {
                // Arrange,
                var referenceObject = A.Dummy<" + TypeNameToken + @">();

                // Act
                var actual = Record.Exception(() => " + NewObjectForArgumentWhiteSpaceTestToken + @");

                // Assert
                actual.Should().BeOfType<ArgumentException>();
                actual.Message.Should().Contain(""" + ConstructorParameterToken + @""");
                actual.Message.Should().Contain(""white space"");
            }";

        private const string PropertyGetterTestMethodTemplate = @"
            [Fact]
            public static void " + PropertyNameToken + @"___Should_return_same_" + ConstructorParameterToken + @"_parameter_passed_to_constructor___When_getting()
            {
                // Arrange,
                var referenceObject = A.Dummy<" + TypeNameToken + @">();
                var systemUnderTest = " + NewObjectForGetterTestToken + @";
                var expected = referenceObject." + PropertyNameToken + @";
                
                // Act
                var actual = systemUnderTest." + PropertyNameToken + @";

                // Assert
                " + AssertPropertyGetterToken + @"
            }";

        public static string GenerateNewLogicCodeForTypeWithSources(
            this Type type,
            Dictionary<string, string> propertyNameToSourceCodeMap)
        {
            type.Named(nameof(type)).Must().NotBeNull();
            propertyNameToSourceCodeMap.Named(nameof(propertyNameToSourceCodeMap)).Must().NotBeNull();

            if (type.GetConstructors()
                    .Any(
                         _ =>
                         {
                             var parameters = _.GetParameters();
                             var nonMatchingParameters = parameters.Select(p => p.Name)
                                                                   .SymmetricDifference(propertyNameToSourceCodeMap.Keys.Select(k => k.ToLowerFirstLetter()))
                                                                   .ToList();

                             return nonMatchingParameters.Count == 0;
                         }))
            {
                var parameterPadding = "                                 ";
                return "new " + type.TreatedTypeName() + "(" + Environment.NewLine + parameterPadding + string.Join("," + Environment.NewLine + parameterPadding, propertyNameToSourceCodeMap.Values) + ")";
            }
            else if (type.GetPropertiesOfConcernFromType().All(_ => _.CanWrite))
            {
                return "new "
                     + type.TreatedTypeName()
                     + "{"
                     + string.Join(", ", propertyNameToSourceCodeMap.Select(_ => Invariant($"{_.Key} = {_.Value}")))
                     + "}";
            }
            else
            {
                var propertiesAddIn = string.Join(",", type.GetPropertiesOfConcernFromType().Select(_ => _.Name));
                throw new NotSupportedException("Could not find a constructor to take properties of concern and they are not all settable: " + propertiesAddIn);
            }
        }

        public static string GenerateConstructorTestMethods(
            this Type type)
        {
            type.Named(nameof(type)).Must().NotBeNull();

            var constructorWithParameters = type.GetConstructors().SingleOrDefault(_ => _.GetParameters().Length > 0);
            var testMethods = new List<string>();
            if (constructorWithParameters != null)
            {
                // since we have parameters we'll go ahead and pad down.
                testMethods.Add(string.Empty);

                var parameters = constructorWithParameters.GetParameters();
                foreach (var parameter in parameters.Where(_ => !_.ParameterType.IsValueType || _.ParameterType == typeof(string)))
                {
                    var propertyNameToSourceCodeMap = parameters.ToDictionary(
                        k => k.Name,
                        v =>
                        {
                            var referenceObject = "referenceObject." + v.Name.ToUpperFirstLetter();
                            return v.Name == parameter.Name ? "null" : referenceObject;
                        });

                    var newObjectCode = type.GenerateNewLogicCodeForTypeWithSources(propertyNameToSourceCodeMap);

                    var testMethod = ConstructorTestMethodForArgumentCodeTemplate
                                    .Replace(TypeNameToken,             type.TreatedTypeName())
                                    .Replace(ConstructorParameterToken, parameter.Name)
                                    .Replace(NewObjectForArgumentNullTestToken, newObjectCode);
                    testMethods.Add(testMethod);

                    if (parameter.ParameterType == typeof(string))
                    {
                        var stringPropertyNameToSourceCodeMap = parameters.ToDictionary(
                            k => k.Name,
                            v =>
                            {
                                var referenceObject = "referenceObject." + v.Name.ToUpperFirstLetter();
                                return v.Name == parameter.Name ? "Invariant($\"  {Environment.NewLine}  \")" : referenceObject;
                            });

                        var stringNewObjectCode = type.GenerateNewLogicCodeForTypeWithSources(stringPropertyNameToSourceCodeMap);

                        var stringTestMethod = ConstructorTestMethodForStringArgumentCodeTemplate
                                              .Replace(TypeNameToken,                     type.TreatedTypeName())
                                              .Replace(ConstructorParameterToken,         parameter.Name)
                                              .Replace(NewObjectForArgumentWhiteSpaceTestToken, stringNewObjectCode);

                        testMethods.Add(stringTestMethod);
                    }
                }

                foreach (var parameter in parameters)
                {
                    var propertyNameToSourceCodeMap = parameters.ToDictionary(
                        k => k.Name,
                        v => "referenceObject." + v.Name.ToUpperFirstLetter());

                    var newObjectCode = type.GenerateNewLogicCodeForTypeWithSources(propertyNameToSourceCodeMap);

                    var assertPropertyGetterToken = parameter.ParameterType.GenerateFluentEqualityStatement(
                        "actual",
                        "expected");

                    var testMethod = PropertyGetterTestMethodTemplate
                                    .Replace(TypeNameToken,               type.TreatedTypeName())
                                    .Replace(PropertyNameToken,           parameter.Name.ToUpperFirstLetter())
                                    .Replace(ConstructorParameterToken,  parameter.Name)
                                    .Replace(AssertPropertyGetterToken,  assertPropertyGetterToken)
                                    .Replace(NewObjectForGetterTestToken, newObjectCode);
                    testMethods.Add(testMethod);
                }
            }

            var constructorTestInflationToken = string.Join(Environment.NewLine, testMethods);

            var result = ConstructingTestMethodsCodeTemplate
                        .Replace(TypeNameToken,                 type.TreatedTypeName())
                        .Replace(ConstructorTestInflationToken, constructorTestInflationToken);
            return result;
        }
    }
}