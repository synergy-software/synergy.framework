using NUnit.Framework;
using Synergy.Contracts.Samples.Domain;

namespace Synergy.Contracts.Test.Failures
{
    [TestFixture]
    public class FailEnumTest
    {
        #region Fail.BecauseEnumOutOfRange

        [Test]
        public void BecauseEnumOutOfRange()
        {
            // ARRANGE
            ContractorType unsupportedEnumValue = 0;

            // ACT
            var exception = Fail.BecauseEnumOutOfRange(unsupportedEnumValue);

            // ASSERT
            Assert.That(exception.Message, Is.EqualTo("Unsupported ContractorType value: 0"));
        }


        [Test]
        public void BecauseEnumOutOfRangeSample()
        {
            // ARRANGE
            var contractor = new Contractor
            {
                Type = 0
            };

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                () => contractor.GetName()
            );

            // ASSERT
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo("Unsupported ContractorType value: 0"));
        }

        #endregion
    }
}