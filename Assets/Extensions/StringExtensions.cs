﻿using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Additions.Extensions
{
    public static class StringExtensions
    {
        private static T DoIf<T>(this T source, bool conditional, Func<T, T> action) => conditional ? action(source) : source;

        /// <summary>
        /// Convert the first character of the string to uppercase.
        /// </summary>
        /// <param name="source">String to convert.</param>
        /// <returns>Converted string.</returns>
        public static string FirstCharToUpper(this string source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return source.FirstCharTo(char.ToUpper, e => e.ToUpper());
        }

        /// <summary>
        /// Convert the first character of the string to lowercase.
        /// </summary>
        /// <param name="source">String to convert.</param>
        /// <returns>Converted string.</returns>
        public static string FirstCharToLower(this string source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return source.FirstCharTo(char.ToLower, e => e.ToLower());
        }

        private static string FirstCharTo(this string source, Func<char, char> charFunc, Func<string, string> stringFunc) => source.Length > 1 ? charFunc(source[0]) + source.Substring(1) : stringFunc(source);

        /// <summary>
        /// Convert the string to sentence case. Remove all uppercases but add uppercase to the first character.
        /// </summary>
        /// <param name="source">String to convert.</param>
        /// <returns>Converted string.</returns>
        /// <example>the Quick Brown Fox Jumps Over The Lazy Dog. -> The quick brown fox jumps over the lazy dog.</example>
        public static string ToSentenceCase(this string source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return source.ToLower().FirstCharToUpper();
        }

        /// <summary>
        /// Split the string in each non-first, non-last uppercase. Only the last consecutive uppercase it splitted.
        /// Convert the string from camel case to common.
        /// Does the same as <see cref="SplitByPascalCase(string)"/>.
        /// </summary>
        /// <param name="source">String to convert.</param>
        /// <paramref name="firstCharToUpper"/>If the first letter should be converted to uppercase through <see cref="FirstCharToUpper(string)"/>.</param>
        /// <returns>Converted string.</returns>
        /// <example>
        ///     • camelCase -> camel Case<br>
        ///     • PascalCase -> Pascal Case<br>
        ///     • HTMLCase -> HTML Case
        /// </example>
        public static string SplitByCamelCase(this string source, bool firstCharToUpper)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return Regex.Replace(source, "([[A-z0-9_]+)([A-Z])", "$1 $2").DoIf(firstCharToUpper, FirstCharToUpper);
        }

        /// <summary>
        /// Split the string in each non-first, non-last uppercase. Only the last consecutive uppercase it splitted.
        /// Convert the string from pascal case to common.
        /// Does the same as <see cref="SplitByCamelCase(string)"/>.
        /// </summary>
        /// <param name="source">String to convert.</param>
        /// <paramref name="firstCharToUpper"/>If the first letter should be converted to uppercase through <see cref="FirstCharToUpper(string)"/>.</param>
        /// <returns>Converted string.</returns>
        /// <example>
        ///     • camelCase -> camel Case<br>
        ///     • PascalCase -> Pascal Case<br>
        ///     • HTMLCase -> HTML Case
        /// </example>
        public static string SplitByPascalCase(this string source, bool firstCharToUpper)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return source.SplitByCamelCase(firstCharToUpper);
        }

        /// <summary>
        /// Split the string in each non-first, non-last uppercase.
        /// Convert the string from title case to common.
        /// </summary>
        /// <param name="source">String to convert.</param>
        /// <paramref name="firstCharToUpper"/>If the first letter should be converted to uppercase through <see cref="FirstCharToUpper(string)"/>.</param>
        /// <returns>Converted string.</returns>
        /// <example>
        ///     • camelCase -> camel Case<br>
        ///     • PascalCase -> Pascal Case<br>
        ///     • HTMLCase -> H T M L Case
        /// </example>
        public static string SplitByTitleCase(this string source, bool firstCharToUpper)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return Regex.Replace(source, "([A-Z])", "$1 ").Trim().DoIf(firstCharToUpper, FirstCharToUpper);
        }

        private static string SplitBySomething(this string source, string spliter, bool firstCharToUpper)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return Regex.Replace(source, $"([A-z0-9])({spliter}+)([A-z0-9])", "$1 $3").DoIf(firstCharToUpper, FirstCharToUpper);
        }

        /// <summary>
        /// Split the string in each non-first, non-last underscore. Treats all consecutive underscores as one.<br>
        /// Convert the string from snake case to common.
        /// </summary>
        /// <param name="source">String to convert.</param>
        /// <paramref name="firstCharToUpper"/>If the first letter should be converted to uppercase through <see cref="FirstCharToUpper(string)"/>.</param>
        /// <returns>Converted string.</returns>
        /// <example>
        ///     • snake_case -> snake case<br>
        ///     • _snake_case -> snake case<br>
        ///     • snake_case_ -> snake case<br>
        ///     • snake__case -> snake case
        /// </example>
        public static string SplitBySnakeCase(this string source, bool firstCharToUpper)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return source.SplitBySomething("_", firstCharToUpper);
        }

        /// <summary>
        /// Split the string in each non-first, non-last middle score. Treats all consecutive middle score as one.<br>
        /// Convert the string from kebab case to common.
        /// </summary>
        /// <param name="source">String to convert.</param>
        /// <paramref name="firstCharToUpper"/>If the first letter should be converted to uppercase through <see cref="FirstCharToUpper(string)"/>.</param>
        /// <returns>Converted string.</returns>
        /// <example>
        ///     • kebab-case -> kebab case<br>
        ///     • -kebab-case -> kebab case<br>
        ///     • kebab-case_ -> kebab case<br>
        ///     • kebab--case -> kebab case
        /// </example>
        public static string SplitByKebabCase(this string source, bool firstCharToUpper)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return source.SplitBySomething("-", firstCharToUpper);
        }

        /// <summary>
        /// Capitalize each word delimited by whitespace
        /// </summary>
        /// <param name="source">String to convert.</param>
        /// <returns>Converted string.</returns>
        /// <example>the quick brown fox jumps over the lazy dog ->  The Quick Brown Fox Jumps Over The Lazy Dog</example>
        public static string ToCapitalWords(this string source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return source.FirstCharToUpperOfEachWord(" ");
        }

        /// <summary>
        /// Capitalize each word delimited by whitespace and join them together.
        /// Does the same as <see cref="ToTileCase(string)"/>.
        /// </summary>
        /// <param name="source">String to convert.</param>
        /// <returns>Converted string.</returns>
        /// <example>
        ///     • pascal case -> PascalCase<br>
        ///     • pAscal CaSe -> PascalCase<br>
        /// </example>
        public static string ToPascalCase(this string source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return source.ToLower().FirstCharToUpperOfEachWord("");
        }

        /// <summary>
        /// Capitalize each word delimited by whitespace and join them together.
        /// Does the same as <see cref="ToPascalCase(string)"/>.
        /// </summary>
        /// <param name="source">String to convert.</param>
        /// <returns>Converted string.</returns>
        /// <example>
        ///     • title case -> TitleCase<br>
        ///     • tItle caSe -> TitleCase<br>
        /// </example>
        public static string ToTitleCase(this string source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return source.ToPascalCase();
        }

        /// <summary>
        /// Capitalize each word delimited by whitespace and join them together, except the first word which remains in lowercase.
        /// </summary>
        /// <param name="source">String to convert.</param>
        /// <returns>Converted string.</returns>
        /// <example>
        ///     • camel case -> cameCase<br>
        ///     • cAmel caSe -> cameCase<br>
        /// </example>
        public static string ToCamelCase(this string source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return source.ToPascalCase().FirstCharToLower();
        }

        /// <summary>
        /// Convert to lowercase and join each word delimited by whitespace with underscores.
        /// </summary>
        /// <param name="source">String to convert.</param>
        /// <returns>Converted string.</returns>
        /// <example>Snake Case -> snake_case</example>
        public static string ToSnakeCase(this string source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return source.JoinWhitespacesToLowerCase("_");
        }

        /// <summary>
        /// Convert to lowercase and join each word delimited by whitespace with underscores.
        /// </summary>
        /// <param name="source">String to convert.</param>
        /// <returns>Converted string.</returns>
        /// <example>Kebab Case -> kebab-case</example>
        public static string ToKebabCase(this string source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return source.JoinWhitespacesToLowerCase("-");
        }

        private static string JoinWhitespacesToLowerCase(this string source, string delimiter) => string.Join(delimiter, source.ToLower().Split(' '));

        private static string FirstCharToUpperOfEachWord(this string source, string delimiter)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            MatchEvaluator matchEvaluator = new MatchEvaluator((match) => FirstCharToUpper(match, delimiter));
            return Regex.Replace(source, "([A-z0-9]+) ([A-z0-9]+)", matchEvaluator);
        }

        private static string FirstCharToUpper(Match match, string delimiter) => string.Join(delimiter, match.Groups.Cast<Group>().Skip(1).Select(e => e.Value.FirstCharToUpper()));

        /// <summary>
        /// Emulate <see cref="SerializedProperty.displayName"/> calling <see cref="SplitByCamelCase(string)"/>, <see cref="SplitBySnakeCase(string)"/>, <see cref="ToCapitalWords(string)"/>.
        /// </summary>
        /// <param name="source">String to convert.</param>
        /// <returns>Converted string.</returns>
        [Obsolete("Use ObjectNames.NicifyVariableName method instead.")]
        public static string ToDisplayUnity(this string source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            return source.SplitByCamelCase(false).SplitBySnakeCase(false).ToCapitalWords();
        }
    }
}