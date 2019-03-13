using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailStringTest
    {
        #region Fail.IfArgumentEmpty

        [Test]
        [TestCaseSource(nameof(FailStringTest.GetEmpty))]
        public void IfArgumentEmptyWithNull(string argumentValue)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentEmpty(argumentValue, nameof(argumentValue))
            );
            
            // ASSERT
            Assert.That(exception, Is.Not.Null);
        }

        [Test]
        [TestCaseSource(nameof(FailStringTest.GetNotEmpty))]
        public void IfArgumentEmptySuccess([NotNull] string argumentValue)
        {
            // ACT
            Fail.IfArgumentEmpty(argumentValue, nameof(argumentValue));
        }

        #endregion

        #region Fail.IfEmpty

        [Test]
        [TestCaseSource(nameof(FailStringTest.GetEmpty))]
        public void IfEmptyWithName(string text)
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfEmpty(text, nameof(text))
                );
        }

        [Test]
        [TestCaseSource(nameof(FailStringTest.GetNotEmpty))]
        public void IfEmptyWithNameSuccess([NotNull] string text)
        {
            Fail.IfEmpty(text, nameof(text));
        }

        [Test]
        [TestCaseSource(nameof(FailStringTest.GetEmpty))]
        public void IfEmptyWithMessage(string text)
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfEmpty(text, Violation.Of("it shouldn't be empty"))
            );
        }

        [Test]
        [TestCaseSource(nameof(FailStringTest.GetNotEmpty))]
        public void IfEmptyWithMessageSuccess([NotNull] string text)
        {
            Fail.IfEmpty(text, Violation.Of("it shouldn't be empty"));
        }

        #endregion

        #region Fail.IfArgumentWhiteSpace

        [Test]
        [TestCaseSource(nameof(FailStringTest.GetWhitespaces))]
        public void IfArgumentWhiteSpace(string text)
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentWhiteSpace(text, nameof(text))
            );
        }

        [Test]
        [TestCaseSource(nameof(FailStringTest.GetNonWhitespaces))]
        public void IfArgumentWhiteSpaceSuccess([NotNull] string text)
        {
            Fail.IfArgumentWhiteSpace(text, nameof(text));
        }

        #endregion

        [Test]
        public void OrFailIfWhiteSpace()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => ((string)null).OrFailIfWhiteSpace("null")
            );

            Assert.Throws<DesignByContractViolationException>(
                () => "".OrFailIfWhiteSpace("empty")
            );

            Assert.Throws<DesignByContractViolationException>(
                () => "   ".OrFailIfWhiteSpace("bia³e-znaki")
            );

            "nie pusty".OrFailIfWhiteSpace("nie-pusty");
        }

        #region Fail.IfWhitespace

        [Test]
        [TestCaseSource(nameof(FailStringTest.GetWhitespaces))]
        public void IfWhitespaceWithName(string text)
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfWhitespace(text, nameof(text))
                );
        }

        [Test]
        [TestCaseSource(nameof(FailStringTest.GetNonWhitespaces))]
        public void IfWhitespaceWithNameSuccess([NotNull] string text)
        {
            Fail.IfWhitespace(text, nameof(text));
        }

        [Test]
        [TestCaseSource(nameof(FailStringTest.GetWhitespaces))]
        public void IfWhitespaceWithMessage(string text)
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfWhitespace(text, Violation.Of("it is whitespace"))
            );
        }

        [Test]
        [TestCaseSource(nameof(FailStringTest.GetNonWhitespaces))]
        public void IfWhitespaceWithMessageSuccess([NotNull] string text)
        {
            Fail.IfWhitespace(text, Violation.Of("it is whitespace"));
        }

        #endregion

        [Test]
        public void IfTooLong()
        {
            var veryLong = "very long";
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfTooLong(veryLong, 3, nameof(veryLong))
            );

            Fail.IfTooLong(null, 300, "null");
            Fail.IfTooLong("aa", 300, "aa");
            Fail.IfTooLong("aa", 2, "aa");
        }

        [Test]
        public void OrFailIfTooLong()
        {
            var veryLong = "very long";
            Assert.Throws<DesignByContractViolationException>(
                () => veryLong.OrFailIfTooLong(3, nameof(veryLong))
            );

            ((string)null).OrFailIfTooLong(300, "null");
            "aa".OrFailIfTooLong(300, "aa");
            "aa".OrFailIfTooLong(2, "aa");
        }

        [Test]
        public void IfTooLongOrWhitespace()
        {
            var veryLong = "very long";
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfTooLongOrWhitespace(veryLong, 3, nameof(veryLong))
            );

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfTooLongOrWhitespace(null, 3, nameof(veryLong))
            );

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfTooLongOrWhitespace(" ", 3, nameof(veryLong))
            );

            Fail.IfTooLongOrWhitespace("a ", 2, "a ");
            Fail.IfTooLongOrWhitespace("aa", 300, "aa");
            Fail.IfTooLongOrWhitespace("aa", 2, "aa");
        }

        [ItemCanBeNull]
        private static IEnumerable<string> GetWhitespaces()
        {
            yield return null;
            yield return "";
            yield return "   ";
            yield return "\t \n";
        }

        [ItemCanBeNull]
        private static IEnumerable<string> GetNonWhitespaces()
        {
            yield return "nie pusty";
            yield return " spacious \t";
        }

        [ItemCanBeNull]
        private static IEnumerable<string> GetEmpty()
        {
            yield return null;
            yield return "";
        }

        [ItemCanBeNull]
        private static IEnumerable<string> GetNotEmpty()
        {
            yield return "not empty";
            yield return " ";
        }
    }
}