using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailCollectionTest
    {
        #region Fail.IfCollectionContains()

        [Test]
        public void IfCollectionContains()
        {
            var kolekcja = new[] {new object(), "element"};

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfCollectionContains(kolekcja, e => object.Equals(e, "element"), "ta kolekcja ma 'ala'")
            );

            Fail.IfCollectionContains(kolekcja, e => object.Equals(e, "dziwny"), "ta kolekcja NIE ma elementu dziwnego");
        }

        #endregion

        #region Fail.IfCollectionContainsNull()

        [Test]
        public void IfCollectionContainsNull()
        {
            var zNullem = new[] {new object(), null};
            IEnumerable<string> pełna = Enumerable.Repeat("element", 2);

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfCollectionContainsNull(zNullem, "zNullem")
            );

            Fail.IfCollectionContainsNull(pełna, "pełna");
        }

        #endregion

        #region Fail.IfCollectionEmpty()

        [Test]
        public void IfCollectionEmpty()
        {
            IEnumerable<object> pusta = Enumerable.Empty<object>();
            IEnumerable<object> nullowata = null;
            IEnumerable<string> pełna = Enumerable.Repeat("element", 2);

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfCollectionEmpty(pusta, "collection")
            );

            Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => Fail.IfCollectionEmpty(nullowata, "collection")
            );

            Fail.IfCollectionEmpty(pełna, "collection");
        }

        [Test]
        public void OrFailIfEmpty()
        {
            IEnumerable<object> pusta = Enumerable.Empty<object>();
            IEnumerable<object> nullowata = null;
            IEnumerable<string> pełna = Enumerable.Repeat("element", 2);

            Assert.Throws<DesignByContractViolationException>(
                () => pusta.OrFaifIfCollectionEmpty("collection")
            );

            Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ExpressionIsAlwaysNull
                () => nullowata.OrFaifIfCollectionEmpty("collection")
            );

            pełna.OrFaifIfCollectionEmpty("collection");
        }

        #endregion

        #region Fail.IfCollectionsAreNotEquivalent()

        [Test]
        public void IfCollectionsAreNotEquivalent()
        {
            var kolekcja1 = new[] {"ala", "olo"};
            var kolekcja1InnaKolejność = new[] {"olo", "ala"};
            var kolekcja2 = new[] {"ala", "inna"};
            var pusta1 = new string[0];
            // ReSharper disable once CollectionNeverUpdated.Local
            var pusta2 = new List<string>();

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfCollectionsAreNotEquivalent(kolekcja1, kolekcja2, "są różne")
            );

            Fail.IfCollectionsAreNotEquivalent(kolekcja1, kolekcja1InnaKolejność, "są różne");
            Fail.IfCollectionsAreNotEquivalent(pusta1, pusta2, "są różne");
        }

        #endregion
    }
}