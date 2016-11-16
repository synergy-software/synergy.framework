using NUnit.Framework;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailBooleanTest
    {
        #region Fail.IfFalse

        [Test]
        public void IfFalseWith0Arguments()
        {
            // ARRANGE
            bool someFalseValue = false;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                () => Fail.IfFalse(someFalseValue, "this should be true")
                );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("this should be true"));
        }

        [Test]
        public void IfFalseWith1Arguments()
        {
            // ARRANGE
            bool someFalseValue = false;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                () => Fail.IfFalse(someFalseValue, "this should be true {0}", 1)
                );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("this should be true 1"));
        }

        [Test]
        public void IfFalseWith2Arguments()
        {
            // ARRANGE
            bool someFalseValue = false;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                () => Fail.IfFalse(someFalseValue, "this should be true {0} {1}", 1, "wow")
                );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("this should be true 1 wow"));
        }

        [Test]
        public void IfFalseWith3Arguments()
        {
            // ARRANGE
            bool someFalseValue = false;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                () => Fail.IfFalse(someFalseValue, "this should be true {0} {1} {2}", 1, "wow", "yep")
                );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("this should be true 1 wow yep"));
        }

        [Test]
        public void IfFalseWithNArguments()
        {
            // ARRANGE
            bool someFalseValue = false;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                // ReSharper disable once HeapView.BoxingAllocation
                () => Fail.IfFalse(someFalseValue, "this should be true {0} {1} {2} {3}", 1, "wow", "yep", "bad")
                );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("this should be true 1 wow yep bad"));
        }

        [Test]
        public void IfFalseSuccess()
        {
            // ARRANGE
            var someTrueValue = true;

            // ACT
            Fail.IfFalse(someTrueValue, "this should be true");
        }

        #endregion

        #region Fail.IfTrue

        [Test]
        public void IfTrueWith0Arguments()
        {
            // ARRANGE
            var someTrueValue = true;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                () => Fail.IfTrue(someTrueValue, "this should be false")
                );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("this should be false"));
        }

        [Test]
        public void IfTrueWith1Arguments()
        {
            // ARRANGE
            var someTrueValue = true;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                () => Fail.IfTrue(someTrueValue, "this should be false {0}", 1)
                );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("this should be false 1"));
        }

        [Test]
        public void IfTrueWith2Arguments()
        {
            // ARRANGE
            var someTrueValue = true;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                () => Fail.IfTrue(someTrueValue, "this should be false {0} {1}", 1, "horrible")
                );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("this should be false 1 horrible"));
        }

        [Test]
        public void IfTrueWith3Arguments()
        {
            // ARRANGE
            var someTrueValue = true;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                () => Fail.IfTrue(someTrueValue, "this should be false {0} {1} {2}", 1, "horrible", "ugly")
                );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("this should be false 1 horrible ugly"));
        }

        [Test]
        public void IfTrueWithNArguments()
        {
            // ARRANGE
            var someTrueValue = true;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                // ReSharper disable once HeapView.BoxingAllocation
                () => Fail.IfTrue(someTrueValue, "this should be false {0} {1} {2} {3}", 1, "horrible", "ugly", "hack")
                );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("this should be false 1 horrible ugly hack"));
        }

        [Test]
        public void IfTrueSuccess()
        {
            // ARRANGE
            var someFalseValue = false;

            // ACT
            Fail.IfTrue(someFalseValue, "this should be false");
        }

        #endregion
    }
}