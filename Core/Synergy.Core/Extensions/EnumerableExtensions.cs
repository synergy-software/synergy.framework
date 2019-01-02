using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Synergy.Contracts;

// ReSharper disable once CheckNamespace
namespace Synergy.Core.Extensions
{
    // ReSharper disable PossibleMultipleEnumeration

    /// <summary>
    /// Extension methods for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Checks if the specified sequence is empty.
        /// </summary>
        [Pure]
        public static bool IsEmpty<T>([NotNull] this IEnumerable<T> collection)
        {
            Fail.IfNull(collection, nameof(collection));

            return collection.Any() == false;
        }

        /// <summary>
        ///     Checks if the specified sequence is NOT empty.
        /// </summary>
        [Pure]
        public static bool IsNotEmpty<T>([NotNull] this IEnumerable<T> collection)
        {
            Fail.IfNull(collection, nameof(collection));

            return collection.Any();
        }

        /// <summary>
        ///     Determines whether any element of a sequence satisfies a condition.
        /// </summary>
        public static bool Contains<T>([NotNull] this IEnumerable<T> collection, [NotNull] Func<T, bool> predicate)
        {
            Fail.IfArgumentNull(collection, nameof(collection));
            Fail.IfArgumentNull(predicate, nameof(predicate));

            return collection.Any(predicate);
        }

        /// <summary>
        /// Determines whether the element exists IN the specified <paramref name="collection"/>.
        /// </summary>
        [Pure]
        public static bool In<T>([NotNull] this T t, [NotNull] IEnumerable<T> collection)
        {
            Fail.IfArgumentNull(t, nameof(t));
            Fail.IfArgumentNull(collection, nameof(collection));

            return collection.Contains(t);
        }

        /// <summary>
        /// Determines whether the element exists IN the specified <paramref name="collection"/>.
        /// </summary>
        [Pure]
        public static bool In<T>([NotNull] this T t, [NotNull] params T[] collection)
        {
            Fail.IfArgumentNull(t, nameof(t));
            Fail.IfArgumentNull(collection, nameof(collection));

            return collection.Contains(t);
        }

        /// <summary>
        /// Determines whether the element does NOT exist IN the specified <paramref name="collection"/>.
        /// </summary>
        [Pure]
        public static bool NotIn<T>([NotNull] this T t, [NotNull] IEnumerable<T> collection)
        {
            Fail.IfArgumentNull(t, nameof(t));
            Fail.IfArgumentNull(collection, nameof(collection));

            return t.In(collection) == false;
        }

        /// <summary>
        /// Determines whether the element does NOT exist IN the specified <paramref name="collection"/>.
        /// </summary>
        [Pure]
        public static bool NotIn<T>([NotNull] this T t, [NotNull] params T[] collection)
        {
            Fail.IfArgumentNull(t, nameof(t));
            Fail.IfArgumentNull(collection, nameof(collection));

            return t.In(collection) == false;
        }
    }
}