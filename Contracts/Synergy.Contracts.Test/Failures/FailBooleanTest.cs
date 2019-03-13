using NUnit.Framework;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailBooleanTest
    {
        #region Fail.IfFalse

        [Test]
        public void IfFalseWithName()
        {
            // ARRANGE
            bool someFalseValue = false;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                () => Fail.IfFalse(someFalseValue, nameof(someFalseValue))
                );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("'someFalseValue' is false; and it should be true;"));
        }

        [Test]
        public void IfFalseWithMessage()
        {
            // ARRANGE
            bool someFalseValue = false;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                () => Fail.IfFalse(someFalseValue, Violation.Of("this should be true {0}", 1))
                );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("this should be true 1"));
        }

        [Test]
        public void IfFalseSuccess()
        {
            // ARRANGE
            var someTrueValue = true;

            // ACT
            Fail.IfFalse(someTrueValue, nameof(someTrueValue));
            Fail.IfFalse(someTrueValue, Violation.Of("this should be true"));
        }

        #endregion

        #region Fail.IfTrue

        [Test]
        public void IfTrueWithName()
        {
            // ARRANGE
            var someTrueValue = true;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                () => Fail.IfTrue(someTrueValue, nameof(someTrueValue))
                );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("'someTrueValue' is true; and it should be false;"));
        }

        [Test]
        public void IfTrueWithMessage()
        {
            // ARRANGE
            var someTrueValue = true;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                () => Fail.IfTrue(someTrueValue, Violation.Of("this should be false {0}", 1))
                );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("this should be false 1"));
        }
        
        [Test]
        public void IfTrueSuccess()
        {
            // ARRANGE
            var someFalseValue = false;

            // ACT
            Fail.IfTrue(someFalseValue, nameof(someFalseValue));
            Fail.IfTrue(someFalseValue, Violation.Of("this should be false"));
        }

        #endregion
    }
}