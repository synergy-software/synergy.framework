using NUnit.Framework;
using Synergy.Contracts.Samples.Domain;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailTest
    {
        #region Fail.Because

        [Test]
        public void BecauseWith0Arguments()
        {
            // ACT
            DesignByContractViolationException exception = Fail.Because("Always");

            // ASSERT
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo("Always"));
        }

        [Test]
        public void BecauseWith1Argument()
        {
            // ACT
            // ReSharper disable once HeapView.BoxingAllocation
            DesignByContractViolationException exception = Fail.Because("Always {0}", 1);

            // ASSERT
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo("Always 1"));
        }

        [Test]
        public void BecauseWith2Arguments()
        {
            // ACT
            // ReSharper disable once HeapView.BoxingAllocation
            DesignByContractViolationException exception = Fail.Because("Always {0} {1}", "fails", 1);

            // ASSERT
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo("Always fails 1"));
        }

        [Test]
        public void BecauseWith3Arguments()
        {
            // ACT
            // ReSharper disable once HeapView.BoxingAllocation
            DesignByContractViolationException exception = Fail.Because("Always {0} {1} {2}", "fails", 1, "times");

            // ASSERT
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo("Always fails 1 times"));
        }

        [Test]
        public void BecauseWithNArguments()
        {
            // ACT
            // ReSharper disable once HeapView.BoxingAllocation
            DesignByContractViolationException exception = Fail.Because("Always {0} {1} {2} {3}", "fails", 1, "times", "frequently");

            // ASSERT
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo("Always fails 1 times frequently"));
        }

        [Test]
        public void BecauseSample()
        {
            // ARRANGE
            var contractor = new Contractor();

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => contractor.SetPersonName("Marcin", "Celej"));

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("Not implemented yet"));
        }

        #endregion
    }
}