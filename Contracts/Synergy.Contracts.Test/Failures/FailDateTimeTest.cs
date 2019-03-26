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
        #region Fail.IfNotDate

        [Test]
        [TestCaseSource(nameof(FailDateTimeTest.GetDatesWithTime))]
        public void IfNotMidnightWithMessage(DateTime dateTime)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNotDate(dateTime, Violation.Of("date should have no hour nor second"))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("date should have no hour nor second"));
        }

        [Test]
        [TestCaseSource(nameof(FailDateTimeTest.GetDatesWithTime))]
        public void IfNotMidnightWithName(DateTime dateTime)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfNotDate(dateTime, nameof(dateTime))
            );

            // ASSERT
            Assert.That(exception, Is.Not.Null);
            //Assert.That(exception.Message, Is.EqualTo("date should have no hour nor second"));
        }

        [Test]
        [TestCaseSource(nameof(FailDateTimeTest.GetDates))]
        public void IfNotMidnightSuccessWithMessage(DateTime? date)
        {
            // ACT
            Fail.IfNotDate(date, Violation.Of("date should have no hour nor second"));
        }

        [Test]
        [TestCaseSource(nameof(FailDateTimeTest.GetDates))]
        public void IfNotMidnightSuccessWithName(DateTime? date)
        {
            // ACT
            Fail.IfNotDate(date, nameof(date));
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

        #region variable.FailIfNotDate

        [Test]
        [TestCaseSource(nameof(FailDateTimeTest.GetDatesWithTime))]
        public void FailIfNotNullableDate(DateTime? dateTime)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => dateTime.FailIfNotDate(nameof(dateTime))
            );

            // ASSERT
            Assert.That(exception, Is.Not.Null);
            //Assert.That(exception.Message, Is.EqualTo("date should have no hour nor second"));
        }

        [Test]
        [TestCaseSource(nameof(FailDateTimeTest.GetDates))]
        public void FailIfNotNullableDateSuccess(DateTime? date)
        {
            // ACT
            var returned = date.FailIfNotDate(nameof(date));

            // ASSERT
            Assert.That(returned, Is.EqualTo(date));
        }

        [Test]
        [TestCaseSource(nameof(FailDateTimeTest.GetDatesWithTime))]
        public void FailIfNotNullableDate(DateTime dateTime)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => dateTime.FailIfNotDate(nameof(dateTime))
            );

            // ASSERT
            Assert.That(exception, Is.Not.Null);
            //Assert.That(exception.Message, Is.EqualTo("date should have no hour nor second"));
        }

        [Test]
        [TestCaseSource(nameof(FailDateTimeTest.GetDates))]
        public void FailIfNotDateSuccess(DateTime? date)
        {
            // ACT
            var returned = date?.FailIfNotDate(nameof(date));

            // ASSERT
            Assert.That(returned, Is.EqualTo(date));
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
            // ReSharper disable once UnusedVariable
            var date = DateTime.Today.FailIfEmpty(nameof(DateTime.Today));
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