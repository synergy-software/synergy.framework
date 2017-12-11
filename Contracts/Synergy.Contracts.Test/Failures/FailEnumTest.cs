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
            Assert.That(exception.Message, Is.EqualTo("Unsupported enum value: 0 (ContractorType)"));
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
            Assert.That(exception.Message, Is.EqualTo("Unsupported enum value: 0 (ContractorType)"));
        }

        #endregion

        #region Fail.IfEnumNotDefined

        [Test]
        public void IfEnumNotDefined()
        {
            // ARRANGE
            var contractor = new Contractor
            {
                Type = 0
            };

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => Fail.IfEnumNotDefined(contractor.Type)
            );

            // ASSERT
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo("Unsupported enum value: 0 (ContractorType)"));
        }

        [Test]
        public void IfEnumNotDefinedSuccess()
        {
            // ARRANGE
            var contractor = new Contractor
            {
                Type = ContractorType.Company
            };

            // ACT
            Fail.IfEnumNotDefined<ContractorType>(contractor.Type);
        }

        #endregion

        #region CastEnumOrFail

        [Test]
        public void CastEnumOrFail()
        {
            // ARRANGE
            var enumNumericValue = 0;

            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => enumNumericValue.CastOrFail<ContractorType>()
            );

            // ASSERT
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo("Unsupported enum value: 0 (ContractorType)"));

            //Assert.That(enumValue, Is.EqualTo(ContractorType.Company));
        }

        [Test]
        public void CastEnumOrFailSuccess()
        {
            // ARRANGE
            var enumNumericValue = (int)ContractorType.Person;

            // ACT
            var enumValue = enumNumericValue.CastOrFail<ContractorType>();

            // ASSERT
            Assert.That(enumValue, Is.EqualTo(ContractorType.Person));
        }


        [Test]
        public void CastEnumOrFail2()
        {
            // ARRANGE
            ContractorType enumValue = 0;
            
            // ACT
            var exception = Assert.Throws<DesignByContractViolationException>(
                () => enumValue.CastEnumOrFail<ContractorType>(nameof(enumValue))
            );

            // ASSERT
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo("Unsupported enumValue value: 0 (ContractorType)"));

            //Assert.That(enumValue, Is.EqualTo(ContractorType.Company));
        }

        #endregion
    }
}