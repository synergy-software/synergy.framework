using System;
using NUnit.Framework;
using Synergy.Contracts.Samples;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailDateTimeTest
    {
        #region Fail.IfNotMidnight

        [Test]
        public void IfNotMidnight()
        {
            // ARRANGE
            DateTime notMidnight = DateTime.Today.AddSeconds(1000);

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNotMidnight(notMidnight, "date should have no hour nor second")
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("date should have no hour nor second"));
        }

        [Test]
        public void IfNotMidnightSuccess()
        {
            // ACT
            Fail.IfNotMidnight(DateTime.Today, "date should have no hour nor second");
        }

        [Test]
        public void IfNotMidnightSuccessWithNull()
        {
            // ACT
            Fail.IfNotMidnight(null, "date should have no hour nor second");
        }

        [Test]
        public void IfNotMidnightSample()
        {
            // ARRANGE
            IContractorRepository contractorRepository = new ContractorRepository();
            DateTime ratherNotMidnight = DateTime.Now;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                () => contractorRepository.GetContractorsAged(ratherNotMidnight, null));

            // ASSERT
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo("minDate must be a midnight"));
        }

        #endregion

        #region Fail.IfDateEmpty

        [Test]
        public void IfDateEmpty()
        {
            // ARRANGE
            DateTime minDate = DateTime.MinValue;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfEmpty(minDate, nameof(minDate))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("'minDate' is empty = 01/01/0001 00:00:00"));
        }

        [Test]
        public void IfDateEmptySuccess()
        {
            // ACT
            Fail.IfEmpty(DateTime.Today, nameof(DateTime.Today));
        }

        #endregion

        #region Fail.FailIfDateEmpty

        [Test]
        public void FailIfDateEmpty()
        {
            // ARRANGE
            DateTime minDate = DateTime.MinValue;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => minDate.FailIfEmpty(nameof(minDate))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("'minDate' is empty = 01/01/0001 00:00:00"));
        }

        [Test]
        public void FailIfDateEmptySuccess()
        {
            // ACT
            DateTime.Today.FailIfEmpty(nameof(DateTime.Today));
        }

        #endregion
    }
}