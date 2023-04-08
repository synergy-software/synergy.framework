using System.Collections;
using System.Collections.ObjectModel;
using Synergy.Contracts;

namespace Synergy.Catalogue
{
#if INTERNALS
    internal
#else
    public
#endif
        static class EnumerableExtensions
    {
        public static bool IsEmpty<T>(IEnumerable<T> collection)
            => collection.OrFail(nameof(collection)).Any() == false;
        
        public static bool IsNotEmpty<T>(IEnumerable<T> collection)
            => collection.OrFail(nameof(collection)).Any();
        
        public static bool In<T>(this T value, params T[] values)
            => values.Contains(value);
        
        public static bool In<T>(this T value, IEnumerable<T> values)
            => values.Contains(value);
        
        public static bool NotIn<T>(this T value, params T[] values)
            => value.In(values) == false;
        
        public static bool NotIn<T>(this T value, IEnumerable<T> values)
            => value.In(values) == false;

        public static IEnumerable<T> Except<T>(this IEnumerable<T> collection, params T[] items)
            => collection.Except((IEnumerable<T>)items);
        
        public static bool AreEquivalent<T>(this IEnumerable<T> c1, IEnumerable<T> c2)
        {
            return !(c1.Except(c2).Any() || c2.Except(c1).Any());
        }
        
        public static List<T> ToList<T>(this IEnumerable<T> collection, int expectedCapacity)
        {
            var list = new List<T>(expectedCapacity);
            list.AddRange(collection);
            return list;
        }
        
        public static List<TDestination> ConvertAll<TSource, TDestination>(this IEnumerable<TSource> source, Func<TSource, TDestination> converter) 
            => source.OrFail(nameof(source))
                     .Select(converter.OrFail(nameof(converter)))
                     .ToList(source.ExpectedCapacity());

        private static int ExpectedCapacity<TSource>(this IEnumerable<TSource> source)
            => source switch
            {
                ICollection<TSource> collection => collection.Count,
                ICollection collection => collection.Count,
                _ => 2
            };
        
        public static ReadOnlyCollection<T> AsReadOnly<T>(this IEnumerable<T> collection)
            => collection.ToList().AsReadOnly();
    }
}