using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NUnit.Framework;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailCollectionTest
    {
        #region Fail.IfCollectionEmpty()

        [Test]
        [TestCaseSource(nameof(FailCollectionTest.GetEmpty))]
        public void IfCollectionEmptyWithName([CanBeNull] IEnumerable collection)
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfCollectionEmpty(collection, nameof(collection))
            );
        }
        
        [Test]
        [TestCaseSource(nameof(FailCollectionTest.GetEmpty))]
        public void IfCollectionEmptyWithNameCallerArgumentExpression([CanBeNull] IEnumerable collection)
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfCollectionEmpty(collection)
            );
        }

        [Test]
        [TestCaseSource(nameof(FailCollectionTest.GetNotEmpty))]
        public void IfCollectionEmptySuccessWithName(IEnumerable collection)
        {
            Fail.IfCollectionEmpty(collection, nameof(collection));
            Fail.IfCollectionEmpty(collection);
        }

        [Test]
        [TestCaseSource(nameof(FailCollectionTest.GetEmpty))]
        public void IfCollectionEmptyWithMessage([CanBeNull] IEnumerable collection)
        {
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfCollectionEmpty(collection, Violation.Of("collection cannot be null or empty"))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("collection cannot be null or empty"));
        }

        [Test]
        [TestCaseSource(nameof(FailCollectionTest.GetNotEmpty))]
        public void IfCollectionEmptySuccessWithMessage([NotNull] IEnumerable collection)
        {
            Fail.IfCollectionEmpty(collection, Violation.Of("collection cannot be null or empty"));
        }

        #endregion

        #region variable.OrFailIfCollectionEmpty()

        [Test]
        [TestCaseSource(nameof(FailCollectionTest.GetEmpty))]
        public void OrFailIfEmptyWithName([CanBeNull] IEnumerable collection)
        {
            Assert.Throws<DesignByContractViolationException>(
                () => collection.OrFailIfCollectionEmpty(nameof(collection))
            );
        }

        [Test]
        [TestCaseSource(nameof(FailCollectionTest.GetNotEmpty))]
        public void OrFailIfEmptySuccessWithName([NotNull] IEnumerable collection)
        {
            collection.OrFailIfCollectionEmpty(nameof(collection));
        }

        [Test]
        [TestCaseSource(nameof(FailCollectionTest.GetEmpty))]
        public void OrFailIfEmptyWithMessage([CanBeNull] IEnumerable collection)
        {
            var exception= Assert.Throws<DesignByContractViolationException>(
                () => collection.OrFailIfCollectionEmpty(Violation.Of("collection cannot be null or empty"))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("collection cannot be null or empty"));
        }

        [Test]
        [TestCaseSource(nameof(FailCollectionTest.GetNotEmpty))]
        public void OrFailIfEmptySuccessWithMessage([NotNull] IEnumerable collection)
        {
            collection.OrFailIfCollectionEmpty(Violation.Of("collection cannot be null or empty"));
        }

        #endregion

        #region Fail.IfCollectionContainsNull()

        [Test]
        [TestCaseSource(nameof(FailCollectionTest.GetCollectionsWithNull))]
        public void IfCollectionContainsNull([CanBeNull] IEnumerable<object> collection)
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfCollectionContainsNull(collection, nameof(collection))
            );
        }

        [Test]
        [TestCaseSource(nameof(FailCollectionTest.GetCollectionsWithoutNull))]
        public void IfCollectionContainsNullSuccess([CanBeNull] IEnumerable<object> collection)
        {
            Fail.IfCollectionContainsNull(collection, nameof(collection));
        }

        #endregion

        #region Fail.IfCollectionContains()

        [Test]
        [TestCaseSource(nameof(FailCollectionTest.GetCollectionsWithSuchElement))]
        public void IfCollectionContains([NotNull] Tuple<IEnumerable<object>, object> pair)
        {
            var collection = pair.Item1;
            var element = pair.Item2;

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfCollectionContains(collection, e => object.Equals(e, element), Violation.Of("this collection contains '{0}'", element))
            );
        }

        [Test]
        [TestCaseSource(nameof(FailCollectionTest.GetCollectionsWithoutSuchElement))]
        public void IfCollectionContainsSuccess([NotNull] Tuple<IEnumerable<object>, object> pair)
        {
            var collection = pair.Item1;
            var element = pair.Item2;

            Fail.IfCollectionContains(collection, e => object.Equals(e,element), Violation.Of("this collection contains '{0}'", element));
        }

        #endregion

        #region Fail.IfCollectionsAreNotEquivalent()

        [Test]
        [TestCaseSource(nameof(FailCollectionTest.GetNonEquivalentCollections))]
        public void IfCollectionsAreNotEquivalent([NotNull] Tuple<IEnumerable<object>, IEnumerable<object>> pair)
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfCollectionsAreNotEquivalent(pair.Item1, pair.Item2, Violation.Of("collections are different"))
            );
        }

        [Test]
        [TestCaseSource(nameof(FailCollectionTest.GetEquivalentCollections))]
        public void IfCollectionsAreNotEquivalentSuccess([NotNull] Tuple<IEnumerable<object>, IEnumerable<object>> pair)
        {
            Fail.IfCollectionsAreNotEquivalent(pair.Item1, pair.Item2, Violation.Of("collections are different"));
        }

        #endregion

        [NotNull, ItemCanBeNull]
        private static IEnumerable<IEnumerable> GetEmpty()
        {
            yield return Enumerable.Empty<object>();
            yield return new string[0];
            yield return new List<int>();
            yield return null;
        }

        [NotNull, ItemNotNull]
        private static IEnumerable<IEnumerable> GetNotEmpty()
        {
            yield return Enumerable.Repeat("element", 2);
            yield return new string[] {null, null};
            yield return new List<int>{1};
        }

        [NotNull, ItemNotNull]
        private static IEnumerable<IEnumerable<object>> GetCollectionsWithNull()
        {
            yield return new[] {"not-null", null};
            yield return new[] {new object(), null};
        }

        [NotNull, ItemNotNull]
        private static IEnumerable<IEnumerable<object>> GetCollectionsWithoutNull()
        {
            yield return new object[] {"not-null", 1};
            yield return Enumerable.Repeat("element", 2);
        }

        [NotNull, ItemCanBeNull]
        private static IEnumerable<Tuple<IEnumerable<object>, object>> GetCollectionsWithSuchElement()
        {
            yield return new Tuple<IEnumerable<object>, object>(new object[] {"not-null", 1}, 1);
            yield return new Tuple<IEnumerable<object>, object>(Enumerable.Repeat("element", 2), "element");
        }

        [NotNull, ItemCanBeNull]
        private static IEnumerable<Tuple<IEnumerable<object>, object>> GetCollectionsWithoutSuchElement()
        {
            yield return new Tuple<IEnumerable<object>, object>(new object[] {"not-null", 1}, 2);
            yield return new Tuple<IEnumerable<object>, object>(Enumerable.Repeat("element", 2), "something else");
        }

        [NotNull, ItemNotNull]
        private static IEnumerable<Tuple<IEnumerable<object>, IEnumerable<object>>> GetNonEquivalentCollections()
        {
            var collection = new[] {"ala", "olo"};
            var anotherCollection = new List<object> {"olo", "zyta"}.AsReadOnly();
            var empty = new string[0];
            var almostEmpty = new List<string>{null};

            yield return new Tuple<IEnumerable<object>, IEnumerable<object>>(collection, anotherCollection);
            yield return new Tuple<IEnumerable<object>, IEnumerable<object>>(empty, almostEmpty);
        }

        [NotNull, ItemNotNull]
        private static IEnumerable<Tuple<IEnumerable<object>, IEnumerable<object>>> GetEquivalentCollections()
        {
            var collection = new[] {"ala", "olo"};
            var collectionWithDifferentOrder = new List<object> {"olo", "ala"}.AsReadOnly();
            var empty1 = new string[0];
            var empty2 = new List<string>();

            yield return new Tuple<IEnumerable<object>, IEnumerable<object>>(collection, collectionWithDifferentOrder);
            yield return new Tuple<IEnumerable<object>, IEnumerable<object>>(empty1, empty2);
        }
    }
}