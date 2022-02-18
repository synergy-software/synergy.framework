using System;
using System.Text.RegularExpressions;

namespace Synergy.Catalogue
{
#if INTERNAL_STRING_EXTENIONS
    internal
#else
    public
#endif
        static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string? text)
            => String.IsNullOrWhiteSpace(text);

        public static bool IsNullOrEmpty(this string text)
            => String.IsNullOrEmpty(text);

        /// <summary>
        /// Returns null if the string contains only whitespaces. Otherwise it returns trimmed text.
        /// </summary>
        public static string? TrimToNull(this string? text)
        {
            if (text.IsNullOrWhiteSpace())
                return null;

            return text?.Trim();
        }

        public static string Left(this string text, int length)
        {
            return length > text.Length ? text : text.Substring(0, length);
        }
        
        public static string CamelCaseToSentence(string input)
        {
            return Regex.Replace(input, "[a-z][A-Z]", m => $"{m.Value[0]} {char.ToLower(m.Value[1])}");
        }
    }
}