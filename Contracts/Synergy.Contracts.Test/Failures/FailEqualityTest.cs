using NUnit.Framework;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailEqualityTest
    {
        #region Fail.IfEqual()

        [Test]
        public void IfEqualWith0Arguments()
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfEqual("s1", "s1", "values are equal and shouldn't be")
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("values are equal and shouldn't be"));
        }

        [Test]
        public void IfEqualWith1Arguments()
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfEqual(1, 1, "{0} is equal to 1 and shouldn't be", 1)
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("1 is equal to 1 and shouldn't be"));
        }

        [Test]
        public void IfEqualWith2Arguments()
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfEqual(1, 1, "{0} is equal to {1} and shouldn't be", 1, 1)
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("1 is equal to 1 and shouldn't be"));
        }

        [Test]
        public void IfEqualWith3Arguments()
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfEqual(1, 1, "{0} is equal to {1} and shouldn't {2}", 1, 1, "be")
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("1 is equal to 1 and shouldn't be"));
        }

        [Test]
        public void IfEqualWithNArguments()
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable HeapView.BoxingAllocation
                () => Fail.IfEqual(1, 1, "{0} is equal to {1} and shouldn't {2}. {3}", 1, 1, "be", "Seriously?")
                // ReSharper restore HeapView.BoxingAllocation
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("1 is equal to 1 and shouldn't be. Seriously?"));
        }

        [Test]
        public void IfEqualNulls()
        {
            // ARRANGE
            object o1 = null;
            object o2 = null;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable ExpressionIsAlwaysNull
                () => Fail.IfEqual(o1, o2, "values are equal and shouldn't be")
                // ReSharper restore ExpressionIsAlwaysNull
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("values are equal and shouldn't be"));
        }

        [Test]
        public void IfEqualSuccess()
        {
            // ACT
            Fail.IfEqual("s1", "s2", "values are equal and shouldn't be");
        }

        #endregion

        #region Fail.IfArgumentEqual()

        [Test]
        public void IfArgumentEqual([Values(0)] int argument)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentEqual(argument, argument, nameof(argument))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("Argument 'argument' is equal to 0 and it should NOT be."));
        }

        [Test]
        public void IfArgumentEqualSuccess([Values(1)] int argument)
        {
            // ACT
            Fail.IfArgumentEqual(argument - 1, argument, nameof(argument));
        }

        #endregion

        #region Fail.IfNotEqual()

        [Test]
        public void IfNotEqual()
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNotEqual("s1", "s2", "values differ and should be equal")
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("values differ and should be equal"));
        }

        [Test]
        public void IfNotEqualSuccess()
        {
            // ACT
            Fail.IfNotEqual("s1", "s1", "values differ and should be equal");
        }

        [Test]
        public void IfNotEqualNulls()
        {
            // ARRANGE
            object o1 = null;
            object o2 = null;

            // ACT
            Fail.IfNotEqual(o1, o2, "values differ and should be equal");
        }

        #endregion
    }
}