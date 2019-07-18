// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SerializationGeneration.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper.Test.CodeGeneration
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using OBeautifulCode.Validation.Recipes;
    using static System.FormattableString;

    public static class SerializationGeneration
    {
        private const string TypeNameToken = "<<<TypeNameHere>>>";

        private const string SerializationConfigurationPrefixToken = "<<<SerializationConfigurationPrefixHere>>>";

        private const string SerializationFieldsCodeTemplate = @"private static readonly ISerializeAndDeserialize BsonSerializer = new NaosBsonSerializer<" + SerializationConfigurationPrefixToken + @"BsonConfiguration>();
        
        private static readonly ISerializeAndDeserialize JsonSerializer = new NaosJsonSerializer<" + SerializationConfigurationPrefixToken + @"JsonConfiguration>();";

        private const string SerializationTestMethodsCodeTemplate = @"
        [SuppressMessage(""Microsoft.Naming"", ""CA1724:TypeNamesShouldNotMatchNamespaces"", Justification = ""Name is correct."")]
        [SuppressMessage(""Microsoft.Design"", ""CA1034:NestedTypesShouldNotBeVisible"", Justification = ""Grouping construct for unit test runner."")]
        public static class Serialization
        {
            [Fact]
            public static void Deserialize___Should_roundtrip_object___When_serializing_and_deserializing_using_NaosJsonSerializer()
            {
                // Arrange
                var expected = A.Dummy<" + TypeNameToken + @">();

                var serializer = JsonSerializer;

                var serializedJson = serializer.SerializeToString(expected);

                // Act
                var actual = serializer.Deserialize<" + TypeNameToken + @">(serializedJson);

                // Assert
                actual.Should().Be(expected);
            }

            [Fact]
            public static void Deserialize___Should_roundtrip_object___When_serializing_and_deserializing_using_NaosBsonSerializer()
            {
                // Arrange
                var expected = A.Dummy<" + TypeNameToken + @">();

                var serializer = BsonSerializer;

                var serializedBson = serializer.SerializeToString(expected);

                // Act
                var actual = serializer.Deserialize<" + TypeNameToken + @">(serializedBson);

                // Assert
                actual.Should().Be(expected);
            }
        }";

        public static string GenerateSerializationTestFields(
            this Type type)
        {
            type.Named(nameof(type)).Should().NotBeNull();

            var prefix = type.Namespace?.Split('.').Last();
            var result = SerializationFieldsCodeTemplate
                        .Replace(TypeNameToken, type.TreatedTypeName())
                        .Replace(SerializationConfigurationPrefixToken, prefix);

            return result;
        }

        public static string GenerateSerializationTestMethods(
            this Type type)
        {
            type.Named(nameof(type)).Should().NotBeNull();
            var result = SerializationTestMethodsCodeTemplate.Replace(TypeNameToken, type.TreatedTypeName());
            return result;
        }
    }
}