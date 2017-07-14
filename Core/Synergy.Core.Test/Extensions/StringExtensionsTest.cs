using JetBrains.Annotations;
using NUnit.Framework;
using Synergy.Core.Extensions;

namespace Synergy.Core.Test.Extensions
{
    [TestFixture]
    public class StringExtensionsTest
    {
        [Test]
        [TestCase("not-empty", false)]
        [TestCase(" ", false)]
        [TestCase("\n", false)]
        [TestCase("\t", false)]
        [TestCase("", true)]
        [TestCase(null, true)]
        public void IsNullOrEmpty(string text, bool expectedResult)
        {
            // ACT
            bool result = text.IsNullOrEmpty();

            // ASSERT
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("not-empty", false)]
        [TestCase(" ", true)]
        [TestCase("\n", true)]
        [TestCase("\t", true)]
        [TestCase("", true)]
        [TestCase(null, true)]
        public void IsNullOrWhiteSpace(string text, bool expectedResult)
        {
            // ACT
            bool result = text.IsNullOrWhiteSpace();

            // ASSERT
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("not-empty", 3, "not")]
        [TestCase("short", 10, "short")]
        [TestCase("will-be-empty", 0, "")]
        [TestCase("", 3, "")]
        public void Left([NotNull] string text, int length, string expectedResult)
        {
            // ACT
            var result = text.Left(length);

            // ASSERT
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("not-empty", "not-empty")]
        [TestCase("trimmed space ", "trimmed space")]
        [TestCase("trimmed tab\t", "trimmed tab")]
        [TestCase("trimmed line\n", "trimmed line")]
        [TestCase("", null)]
        [TestCase(" ", null)]
        [TestCase("\t", null)]
        [TestCase("\n", null)]
        [TestCase("\n \t", null)]
        [TestCase(null, null)]
        public void TrimOrNull([NotNull] string text, string expectedResult)
        {
            // ACT
            var result = text.TrimOrNull();

            // ASSERT
            Assert.AreEqual(expectedResult, result);
        }
    }
}
