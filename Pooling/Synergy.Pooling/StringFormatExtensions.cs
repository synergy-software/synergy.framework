// ------------------------------------------------------------------------
//
// WARN: This file is reused between Synergy projects - change it carefully
// 
// ------------------------------------------------------------------------

using System;
using JetBrains.Annotations;
using Synergy.Pooling;

// ReSharper disable once CheckNamespace

namespace Synergy.Extensions
{
    /// <summary>
    /// Contains extension methods for string objects.
    /// </summary>
#if INTERNAL_POOL
    internal
#else
    public
#endif
            static class StringFormatExtensions
    {
        private static readonly Action<object[]> clearArray = array => Array.Clear(array, 0, array.Length);

        private static readonly Pool<object[]> poolOf1ElementArrays = new Pool<object[]>(
            constructor: () => new object[1],
            initialSize: 16,
            destructor: StringFormatExtensions.clearArray);

        private static readonly Pool<object[]> poolOf2ElementArrays = new Pool<object[]>(
            constructor: () => new object[2],
            initialSize: 16,
            destructor: StringFormatExtensions.clearArray);

        private static readonly Pool<object[]> poolOf3ElementArrays = new Pool<object[]>(
            constructor: () => new object[3],
            initialSize: 16,
            destructor: StringFormatExtensions.clearArray);

        private static readonly Pool<object[]> poolOf4ElementArrays = new Pool<object[]>(
            constructor: () => new object[4],
            initialSize: 16,
            destructor: StringFormatExtensions.clearArray);

        /// <summary>
        /// Formats the specified string using {0} annottations.
        /// </summary>
        [NotNull]
        [StringFormatMethod("format")]
        public static string Format(
            [NotNull] this string format,
            [CanBeNull] object arg1)
        {
            using (Pooled<object[]> args = StringFormatExtensions.poolOf1ElementArrays.Get())
            {
                args.Value[0] = arg1;
                return string.Format(format, args.Value);
            }
        }

        /// <summary>
        /// Formats the specified string using {0}, {1} annottations.
        /// </summary>
        [NotNull]
        [StringFormatMethod("format")]
        public static string Format(
            [NotNull] this string format,
            [CanBeNull] object arg1,
            [CanBeNull] object arg2)
        {
            using (Pooled<object[]> args = StringFormatExtensions.poolOf2ElementArrays.Get())
            {
                args.Value[0] = arg1;
                args.Value[1] = arg2;
                return string.Format(format, args.Value);
            }
        }

        /// <summary>
        /// Formats the specified string using {0}, {1}, {2} annottations.
        /// </summary>
        [NotNull]
        [StringFormatMethod("format")]
        public static string Format(
            [NotNull] this string format,
            [CanBeNull] object arg1,
            [CanBeNull] object arg2,
            [CanBeNull] object arg3)
        {
            using (Pooled<object[]> args = StringFormatExtensions.poolOf3ElementArrays.Get())
            {
                args.Value[0] = arg1;
                args.Value[1] = arg2;
                args.Value[2] = arg3;
                return string.Format(format, args.Value);
            }
        }

        /// <summary>
        /// Formats the specified string using {0}, {1}, {2}, {3} annottations.
        /// </summary>
        [NotNull]
        [StringFormatMethod("format")]

        public static string Format(
            [NotNull] this string format,
            [CanBeNull] object arg1,
            [CanBeNull] object arg2,
            [CanBeNull] object arg3,
            [CanBeNull] object arg4)
        {
            using (Pooled<object[]> args = StringFormatExtensions.poolOf4ElementArrays.Get())
            {
                args.Value[0] = arg1;
                args.Value[1] = arg2;
                args.Value[2] = arg3;
                args.Value[3] = arg4;
                return string.Format(format, args.Value);
            }
        }

        /// <summary>
        /// Formats the specified string using {0}, {1}, {2}, {3}, ... annottations.
        /// </summary>
        [NotNull]
        [StringFormatMethod("format")]
        public static string Format(
            [NotNull] this string format,
            [NotNull] params object[] args)
        {
            return string.Format(format, args);
        }
    }
}