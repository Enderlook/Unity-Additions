using Enderlook.Text;

using System;

namespace Enderlook.Unity.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Emulate <see cref="UnityEditor.SerializedProperty.displayName"/> calling <see cref="Text.StringExtensions.SplitByCamelCase(string)"/>, <see cref="Text.StringExtensions.SplitBySnakeCase(string)"/>, <see cref="Text.StringExtensions.ToCapitalWords(string)"/>.
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