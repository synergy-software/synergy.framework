using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;

namespace Synergy.Sample.Web.API.Services.Infrastructure
{
    public static class CollectionExtensions
    {
        [Pure]
        public static bool IsEmpty<T>([NotNull] this IEnumerable<T> collection) => collection.Any() == false;

        [Pure]
        public static bool IsNotEmpty<T>([NotNull] this IEnumerable<T> collection) => collection.Any();

        public static ReadOnlyCollection<T> AsReadOnly<T>(this IEnumerable<T> collection)
            => collection.ToList().AsReadOnly();

        public static ReadOnlyCollection<TDestination> ConvertAll<TSource, TDestination>(this ReadOnlyCollection<TSource> collection, Func<TSource, TDestination> converter)
            => collection.Select(converter).ToList().AsReadOnly();
    }
}