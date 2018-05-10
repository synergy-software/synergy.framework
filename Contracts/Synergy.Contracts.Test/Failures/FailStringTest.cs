using JetBrains.Annotations;
using NUnit.Framework;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailStringTest
    {
        #region Fail.IfArgumentEmpty

        [Test]
        [TestCase(null)]
        public void IfArgumentEmptyWithNull(string argumentValue)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentEmpty(argumentValue, nameof(argumentValue))
            );
            
            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("Argument 'argumentValue' was null."));
        }

        [Test]
        [TestCase("")]
        public void IfArgumentEmptyWithEmptyString(string argumentValue)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentEmpty(argumentValue, nameof(argumentValue))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("Argument 'argumentValue' was empty."));
        }

        [Test]
        [TestCase("not empty")]
        [TestCase(" ")]
        public void IfArgumentEmptySuccess([NotNull] string argumentValue)
        {
            // ACT
            Fail.IfArgumentEmpty(argumentValue, nameof(argumentValue));
        }

        #endregion

        #region Fail.IfEmpty

        [Test]
        public void IfEmpty()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfEmpty("", "message")
                );

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfEmpty(null, "message")
                );

            Fail.IfEmpty("   ", "message");
            Fail.IfEmpty("aa", "message");
        }

        #endregion

        [Test]
        public void IfArgumentWhiteSpace()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentWhiteSpace(null, "null")
            );

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentWhiteSpace("", "empty")
            );

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentWhiteSpace("   ", "bia³e-znaki")
            );

            Fail.IfArgumentWhiteSpace("nie pusty", "nie-pusty");
        }

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

        [Test]
        public void IfWhitespace()
        {
            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfWhitespace("", "message")
                );

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfWhitespace(null, "message")
                );

            Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfWhitespace("   ", "message")
                );

            Fail.IfWhitespace("aa", "message");
        }

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
    }
}