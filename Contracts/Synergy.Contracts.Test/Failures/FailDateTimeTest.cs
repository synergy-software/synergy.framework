using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;
using Synergy.Contracts.Samples;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailDateTimeTest
    {

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
        public void IfDateEmptyCallerArgumentExpression()
        {
            // ARRANGE
            DateTime minDate = DateTime.MinValue;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfEmpty(minDate)
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("'minDate' is empty = 01/01/0001 00:00:00"));
        }
        
        [Test]
        public void IfDateEmptySuccess()
        {
            // ACT
            Fail.IfEmpty(DateTime.Today, nameof(DateTime.Today));
            Fail.IfEmpty(DateTime.Today);
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
        public void FailIfDateEmptyCallerArgumentExpression()
        {
            // ARRANGE
            DateTime minDate = DateTime.MinValue;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => minDate.FailIfEmpty()
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("'minDate' is empty = 01/01/0001 00:00:00"));
        }

        [Test]
        public void FailIfDateEmptySuccess()
        {
            // ACT
            // ReSharper disable once UnusedVariable
            var date1 = DateTime.Today.FailIfEmpty(nameof(DateTime.Today));
            // ReSharper disable once UnusedVariable
            var date2 = DateTime.Today.FailIfEmpty();
        }

        #endregion

        [ItemCanBeNull]
        private static IEnumerable<DateTime?> GetDates()
        {
            yield return null;
            yield return DateTime.Today;
            yield return DateTime.MinValue;
            //yield return DateTime.MaxValue;
            yield return new DateTime(2019,03,26);

        }

        private static IEnumerable<DateTime> GetDatesWithTime()
        {
            yield return DateTime.MaxValue;
            yield return new DateTime(2019,03,26).AddMilliseconds(1);

        }

    }
}