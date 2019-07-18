// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HashCodeGeneration.cs" company="Naos Project">
//    Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper.Test.CodeGeneration
{
    using System;
    using System.Linq;
    using System.Reflection;
    using OBeautifulCode.Validation.Recipes;
    using static System.FormattableString;

    public static class HashCodeGeneration
    {
        private const string HashToken = "<<<HashMethodStackHere>>>";

        private const string HashMethodCodeTemplate = @"
        /// <inheritdoc />
        public override int GetHashCode() => HashCodeHelper.Initialize()
            ." + HashToken + @"
            .Value;";

        public static string GenerateGetHashCodeMethod(
            this Type type)
        {
            var properties = type.GetPropertiesOfConcernFromType();
            var hashLines = properties.Select(_ => _.GenerateHashCodeMethodCodeForProperty()).ToList();
            var hashToken = string.Join(Environment.NewLine + "            .", hashLines);
            var result = HashMethodCodeTemplate.Replace(HashToken, hashToken);

            return result;
        }

        private static string GenerateHashCodeMethodCodeForProperty(
            this PropertyInfo propertyInfo)
        {
            propertyInfo.Named(nameof(propertyInfo)).Must().NotBeNull();

            if (propertyInfo.PropertyType.IsAssignableToAnyDictionary())
            {
                return Invariant($"HashDictionary(this.{propertyInfo.Name})");
            }
            else if (propertyInfo.PropertyType.IsAssignableToAnyCollection())
            {
                return Invariant($"HashElements(this.{propertyInfo.Name})");
            }
            else if (propertyInfo.PropertyType == typeof(string))
            {
                return Invariant($"Hash(this.{propertyInfo.Name})");
            }
            else
            {
                return Invariant($"Hash(this.{propertyInfo.Name})");
            }
        }
    }
}