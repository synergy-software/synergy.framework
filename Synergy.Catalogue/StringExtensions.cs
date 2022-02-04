using System;
using System.Text.RegularExpressions;

namespace Synergy.Catalogue
{
#if INTERNAL_STRING_EXTENSIONS
#else
    public
#endif
        static class StringExtensions
    {
        public static bool IsNullOrWhitespace(this string? text)
            => String.IsNullOrWhiteSpace(text);

        public static string? TrimToNull(this string? text)
        {
            if (text.IsNullOrWhitespace())
                return null;

            return text?.Trim();
        }

        public static string CamelCaseToSentence(string input)
        {
            return Regex.Replace(input, "[a-z][A-Z]", m => $"{m.Value[0]} {char.ToLower(m.Value[1])}");
        }
    }
}