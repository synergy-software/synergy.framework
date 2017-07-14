using System;
using JetBrains.Annotations;
using Synergy.Contracts;

namespace Synergy.Core.Extensions
{
    public static class StringExtensions
    {
        [Pure]
        [ContractAnnotation("null=>true")]
        public static bool IsNullOrEmpty([CanBeNull] this string text)
        {
            return String.IsNullOrEmpty(text);
        }

        [Pure]
        [ContractAnnotation("null=>true")]
        public static bool IsNullOrWhiteSpace([CanBeNull] this string text)
        {
            return String.IsNullOrWhiteSpace(text);
        }

        [NotNull, Pure]
        [ContractAnnotation("text:null=>halt")]
        public static string Left([NotNull] this string text, int length)
        {
            Fail.IfArgumentNull(text, nameof(text));

            if (text.Length <= length)
                return text;

            return text.Substring(0, length);
        }

        [CanBeNull, Pure]
        [ContractAnnotation("text:null=>null")]
        public static string TrimOrNull([CanBeNull] this string text)
        {
            if (String.IsNullOrWhiteSpace(text))
                return null;

            return text.Trim();
        }
    }
}
