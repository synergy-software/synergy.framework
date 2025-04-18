using System;
using System.Collections;
using JetBrains.Annotations;
using NUnit.Framework;
using Synergy.Contracts.Samples;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailGuidTest
    {
        #region Fail.IfArgumentEmpty

        [TestCaseSource(nameof(FailGuidTest.GetEmptyGuid))]
        public void IfArgumentEmpty(Guid someEmptyGuid)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentEmpty(someEmptyGuid, nameof(someEmptyGuid))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("Argument 'someEmptyGuid' is an empty Guid."));
        }

        [TestCaseSource(nameof(FailGuidTest.GetEmptyGuid))]
        public void IfArgumentEmptyCallerArgumentExpression(Guid someEmptyGuid)
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfArgumentEmpty(someEmptyGuid)
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("Argument 'someEmptyGuid' is an empty Guid."));
        }

        [TestCaseSource(nameof(FailGuidTest.GetNewGuid))]
        public void IfArgumentEmptySuccess(Guid someGuid)
        {
            // ACT
            Fail.IfArgumentEmpty(someGuid, nameof(someGuid));
            Fail.IfArgumentEmpty(someGuid);
        }

        [TestCaseSource(nameof(FailGuidTest.GetEmptyGuid))]
        public void IfArgumentEmptySample(Guid id)
        {
            // ARRANGE
            IContractorRepository contractorRepository = new ContractorRepository();

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                () => contractorRepository.FindContractorByGuid(id)
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo($"Argument '{nameof(id)}' is an empty Guid."));
        }

        #endregion

        #region Fail.IfEmpty

        [Test]
        public void IfEmptyWithName()
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfEmpty(Guid.Empty, "guid")
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("'guid' is empty."));
        }

        [Test]
        public void IfEmptyWithCallerArgumentExpression()
        {
            // ARRANGE
            var guid = Guid.Empty;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfEmpty(guid)
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("'guid' is empty."));
        }

        [Test]
        public void IfEmptySuccess()
        {
            // ARRANGE
            Guid notEmptyGuid = Guid.NewGuid();
            
            // ACT
            Fail.IfEmpty(notEmptyGuid);
        }

        [Test]
        public void IfEmptyWithMessage()
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfEmpty(Guid.Empty, Violation.Of("guid is empty and it shouldn't be"))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("guid is empty and it shouldn't be"));
        }

        [Test]
        public void IfEmptyWithMessageSuccess()
        {
            // ARRANGE
            Guid notEmptyGuid = Guid.NewGuid();

            // ACT
            Fail.IfEmpty(notEmptyGuid, Violation.Of("guid is empty and it shouldn't be"));
        }

        #endregion

        #region variable.FailIfEmpty()

        [Test]
        public void FailIfEmptyWithName()
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Guid.Empty.FailIfEmpty("guid")
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("'guid' is empty."));
        }

        [Test]
        public void FailIfEmptyWithCallerArgumentExpression()
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Guid.Empty.FailIfEmpty()
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("'Guid.Empty' is empty."));
        }

        [Test]
        public void FailIfEmptySuccess()
        {
            // ARRANGE
            Guid notEmptyGuid = Guid.NewGuid();

            // ACT
            notEmptyGuid.FailIfEmpty();
        }
        
        [Test]
        public void FailIfEmptyWithMessage()
        {
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Guid.Empty.FailIfEmpty(Violation.Of("guid is empty and it shouldn't be"))
            );

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("guid is empty and it shouldn't be"));
        }

        [Test]
        public void FailIfEmptyWithMessageSuccess()
        {
            // ARRANGE
            Guid notEmptyGuid = Guid.NewGuid();

            // ACT
            notEmptyGuid.FailIfEmpty(Violation.Of("guid is empty and it shouldn't be"));
        }

        #endregion

        [ItemNotNull, NotNull]
        private static IEnumerable GetEmptyGuid()
        {
            // ReSharper disable once HeapView.BoxingAllocation
            yield return Guid.Empty;
        }

        [ItemNotNull, NotNull]
        private static IEnumerable GetNewGuid()
        {
            // ReSharper disable once HeapView.BoxingAllocation
            yield return Guid.NewGuid();
        }

        [Explicit, Test]
        public void WriteEmptyGuidsToConsole()
        {
            // ReSharper disable RedundantToStringCallForValueType
            Console.WriteLine("Guid.Empty = " + Guid.Empty.ToString());
            Console.WriteLine("new Guid() = " + new Guid().ToString());
            // ReSharper restore RedundantToStringCallForValueType
        }
    }
}