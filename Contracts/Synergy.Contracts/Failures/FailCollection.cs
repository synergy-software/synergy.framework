using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Synergy.Contracts
{
    static partial class Fail
    {
        // TODO:mace (from:mace @ 22-10-2016) public static void IfCollectionDoesNotContain<T>([CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] IEnumerable<T> collection,) 
        // TODO: Marcin Celej [from: Marcin Celej on: 29-05-2023]: Fail.IfCollectionContainsDuplicates

        #region Fail.IfCollectionEmpty()

        /// <summary>
        /// Throws exception when the collection is <see langword="null" /> or empty.
        /// </summary>
        /// <param name="collection">Collection to check against being <see langword="null" /> or empty.</param>
        /// <param name="collectionName">Name of the collection.</param>
        [AssertionMethod]
        [ContractAnnotation("collection: null => halt")]
        public static void IfCollectionEmpty(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
            IEnumerable collection,
#if NET6_0_OR_GREATER
            [System.Runtime.CompilerServices.CallerArgumentExpression("collection")] string? collectionName = null
#else
            string collectionName
#endif
        )
        {
            collection.OrFailIfCollectionEmpty(collectionName);
        }

        /// <summary>
        /// Throws exception when the collection is <see langword="null" /> or empty.
        /// </summary>
        /// <param name="collection">Collection to check against being <see langword="null" /> or empty.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [AssertionMethod]
        [ContractAnnotation("collection: null => halt")]
        public static void IfCollectionEmpty(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
            IEnumerable collection,
            Violation message
        )
        {
            collection.OrFailIfCollectionEmpty(message);
        }

        #endregion

        #region variable.OrFailIfCollectionEmpty()

        /// <summary>
        /// Throws exception when the collection is <see langword="null" /> or empty.
        /// </summary>
        /// <typeparam name="T">Type of the collection</typeparam>
        /// <param name="collection">Collection to be checked against emptiness</param>
        /// <param name="collectionName">Collection name</param>
        /// <returns>The same collection as provided</returns>
        [NotNull] [return: System.Diagnostics.CodeAnalysis.NotNull] 
        [AssertionMethod]
        [ContractAnnotation("collection: null => halt")]
        public static T OrFailIfCollectionEmpty<T>(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
            this T collection,
            [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string collectionName
        )
            where T : IEnumerable
        {
            Fail.RequiresCollectionName(collectionName);

            if (collection == null)
                throw Fail.Because(Violation.WhenCollectionIsNull(collectionName));

            if (collection.IsEmpty())
                throw Fail.Because(Violation.WhenCollectionIsEmpty(collectionName));

            return collection;
        }

        /// <summary>
        /// Throws exception when the collection is <see langword="null" /> or empty.
        /// </summary>
        /// <typeparam name="T">Type of the collection</typeparam>
        /// <param name="collection">Collection to be checked against emptiness</param>
        /// <param name="message">Collection name</param>
        /// <returns>The same collection as provided</returns>
        [NotNull] [return: System.Diagnostics.CodeAnalysis.NotNull] 
        [AssertionMethod]
        [ContractAnnotation("collection: null => halt")]
        public static T OrFailIfCollectionEmpty<T>(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
            this T collection,
            Violation message
        )
            where T : IEnumerable
        {

            if (collection == null)
                throw Fail.Because(message);

            if (collection.IsEmpty())
                throw Fail.Because(message);

            return collection;
        }


        [MustUseReturnValue]
        private static bool IsEmpty([NotNull] [System.Diagnostics.CodeAnalysis.NotNull] this IEnumerable source)
        {
            if (source is ICollection collection)
                return collection.Count == 0;

            return source.GetEnumerator()
                         .MoveNext() == false;
        }

        #endregion

        #region Fail.IfCollectionContainsNull()

        /// <summary>
        /// Throws exception when the collection contains null.
        /// <para>REMARKS: The provided collection CANNOT by <see langword="null"/> as it will throw the exception.</para>
        /// </summary>
        /// <typeparam name="T">Type of the collection element.</typeparam>
        /// <param name="collection">Collection to investigate whether contains null.</param>
        /// <param name="collectionName">Name of the collection</param>
        [AssertionMethod]
        [ContractAnnotation("collection: null => halt")]
        public static void IfCollectionContainsNull<T>(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
            IEnumerable<T> collection,
            [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string collectionName
        )
            where T : class
        {
            Fail.RequiresCollectionName(collectionName);

            if (collection == null)
                throw Fail.Because(Violation.WhenCollectionIsNull(collectionName));

            if (collection.Contains(null))
                throw Fail.Because(Violation.WhenCollectionContainsNull(collectionName));
        }

        #endregion

        #region Fail.IfCollectionContains()

        /// <summary>
        /// Throws the exception when collection contains element meeting specified criteria.
        /// <para>REMARKS: The provided collection CANNOT by <see langword="null"/> as it will throw the exception.</para>
        /// </summary>
        /// <typeparam name="T">Type of the collection element.</typeparam>
        /// <param name="collection">Collection to investigate whether contains specific element.</param>
        /// <param name="func">Function with criteria that at least one element must meet.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [AssertionMethod]
        [ContractAnnotation("collection: null => halt")]
        public static void IfCollectionContains<T>(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
            IEnumerable<T> collection,
            [NotNull] [System.Diagnostics.CodeAnalysis.NotNull] Func<T, bool> func,
            Violation message
        )
        {
            Fail.IfArgumentNull(collection, nameof(collection));
            T element = collection.FirstOrDefault(func);
            Fail.IfNotNull(element, message);
        }

        #endregion

        #region Fail.IfCollectionsAreNotEquivalent()

        /// <summary>
        /// Throws exception when the specified collections are not equivalent. Equivalent collection contain the same elements in any order.
        /// <para>REMARKS: The provided collection CANNOT by <see langword="null"/> as it will throw the exception.</para>
        /// </summary>
        /// <typeparam name="T">Type of the collection element.</typeparam>
        /// <param name="collection1">First collection to compare.</param>
        /// <param name="collection2">Second collection to compare.</param>
        /// <param name="message">Message that will be passed to <see cref="DesignByContractViolationException"/> when the check fails.</param>
        [AssertionMethod]
        [ContractAnnotation("collection1: null => halt; collection2: null => halt")]
        public static void IfCollectionsAreNotEquivalent<T>(
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
            IEnumerable<T> collection1,
            [CanBeNull, AssertionCondition(AssertionConditionType.IS_NOT_NULL)]
            IEnumerable<T> collection2,
            Violation message
        )
        {
            Fail.IfArgumentNull(collection1, nameof(collection1));
            Fail.IfArgumentNull(collection2, nameof(collection2));

            IEnumerable<T> list1 = collection1 as IList<T> ?? collection1.ToList();
            IEnumerable<T> list2 = collection2 as IList<T> ?? collection2.ToList();

            int collection1Count = list1.Count();
            int collection2Count = list2.Count();
            if (collection1Count != collection2Count)
                throw Fail.Because(message);

            bool areEquivalent = list1.Intersect(list2)
                                      .Count() == collection1Count;
            Fail.IfFalse(areEquivalent, message);
        }

        #endregion

        /// <summary>
        /// Checks if collection name was provided.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private static void RequiresCollectionName([NotNull] [System.Diagnostics.CodeAnalysis.NotNull] string collectionName)
        {
            if (string.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException(nameof(collectionName));
        }
    }
}