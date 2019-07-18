// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CodeGenerationShared.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper.Test.CodeGeneration
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using OBeautifulCode.Validation.Recipes;
    using static System.FormattableString;

    public static class CodeGenerationShared
    {
        private const string TypeNameToken = "<<<<TypeNameHere>>>";

        private static readonly Type[] DictionaryTypes = new[]
                                                         {
                                                             typeof(Dictionary<,>),
                                                             typeof(IDictionary<,>),
                                                             typeof(ReadOnlyDictionary<,>),
                                                             typeof(IReadOnlyDictionary<,>),
                                                             typeof(ConcurrentDictionary<,>),
                                                         };

        private static readonly Type[] CollectionTypes = new[]
                                                         {
                                                             typeof(Collection<>),
                                                             typeof(ICollection<>),
                                                             typeof(ReadOnlyCollection<>),
                                                             typeof(IReadOnlyCollection<>),
                                                             typeof(List<>),
                                                             typeof(IList<>),
                                                             typeof(IReadOnlyList<>),
                                                         };

        public static string GenerateFluentEqualityStatement(
            this Type type,
            string actual,
            string expected)
        {
            var result = Invariant($"{actual}.Should().{(type.IsAssignableToAnyDictionary() || type.IsAssignableToAnyCollection() ? "Equal" : "Be")}({expected});");
            return result;
        }

        public static PropertyInfo[] GetPropertiesOfConcernFromType(
            this Type type)
        {
            type.Named(nameof(type)).Must().NotBeNull();

            var result = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.FlattenHierarchy);

            return result;
        }

        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Lowercase is correct here.")]
        public static string TreatedTypeName(
            this Type type)
        {
            type.Named(nameof(type)).Must().NotBeNull();

            if (type == typeof(string))
            {
                return typeof(string).Name.ToLowerInvariant();
            }
            else if (type == typeof(int))
            {
                return "int";
            }
            else if (type == typeof(bool))
            {
                return "bool";
            }
            else if (type.IsGenericType)
            {
                var typeName = type.Name.Split('`').First();
                var genericArguments = type.GetGenericArguments();
                var genericArgumentsTypeNames = genericArguments.Select(_ => _.TreatedTypeName());
                return typeName + "<" + string.Join(", ", genericArgumentsTypeNames) + ">";
            }
            else
            {
                return type.Name;
            }
        }

        public static bool IsAssignableToAnyDictionary(
            this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsGenericType)
            {
                return false;
            }

            var genericType = type.GetGenericTypeDefinition();

            var result = DictionaryTypes.Any(_ => genericType == _);
            return result;
        }

        public static bool IsAssignableToAnyCollection(
            this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsGenericType)
            {
                return false;
            }

            var genericType = type.GetGenericTypeDefinition();

            var result = CollectionTypes.Any(_ => genericType == _);
            return result;
        }

        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Lowercase is correct here.")]
        public static string ToLowerFirstLetter(
            this string input)
        {
            if (input == null)
            {
                return null;
            }

            if (input.Length == 1)
            {
                return input.ToLowerInvariant();
            }

            return input[0].ToString().ToLowerInvariant() + input.Substring(1);
        }

        public static string ToUpperFirstLetter(
            this string input)
        {
            if (input == null)
            {
                return null;
            }

            if (input.Length == 1)
            {
                return input.ToUpperInvariant();
            }

            return input[0].ToString().ToUpperInvariant() + input.Substring(1);
        }
    }
}