using System;
using Synergy.Contracts.Requirements;
using Xunit;

namespace Synergy.Contracts.Test.Requirements
{
    public class BusinessTest
    {
        private const bool Failure = true;
        private const bool OK = false;

        [Fact]
        public void Rule()
        {
            this.Act(() =>
                    Business.Rule("For online payment, transaction amount cannot exceed online payment limit")
                            .Throws(new NotImplementedException("NOT IMPLEMENTED"))
                )
                .AssertException("NOT IMPLEMENTED");
        }
        
        [Theory]
        [InlineData(-10, BusinessTest.Failure)]
        [InlineData(10, BusinessTest.OK)]
        public void Requires(double balance, bool expectedException)
        {
            var exception = expectedException ? $"balance cannot be < 0 and actually is {balance}" : null;

            this.Act(() =>
                    Business.Requires(balance >= 0)
                            .Throws($"balance cannot be < 0 and actually is {balance}")
                )
                .AssertException(exception);
        }
        
        [Theory]
        [InlineData(-10, BusinessTest.Failure)]
        [InlineData(10, BusinessTest.OK)]
        public void Requires_Documented(double balance, bool expectedException)
        {
            var exception = expectedException ? $"balance cannot be < 0 and actually is {balance}" : null;

            this.Act(() =>
                    Business.Requires(balance >= 0)["Account Balance must be >= 0"]
                            .Throws($"balance cannot be < 0 and actually is {balance}")
                )
                .AssertException(exception)
                .AssertRequirementDescription("Account Balance must be >= 0");
        }

        [Theory]
        [InlineData(10, 11, BusinessTest.Failure)]
        [InlineData(10, 10, BusinessTest.OK)]
        [InlineData(null, 10, BusinessTest.OK)]
        public void RequiresWhen(int? withdrawLimit, int withdrawAmount, bool expectedException)
        {
            var exception = expectedException ? $"Withdraw Amount ({withdrawAmount}) exceeds the Withdraw Limit ({withdrawLimit})" : null;

            this.Act(() =>
                    Business.When(withdrawLimit != null)
                            .Requires(withdrawAmount <= withdrawLimit)
                            .Throws($"Withdraw Amount ({withdrawAmount}) exceeds the Withdraw Limit ({withdrawLimit})")
                )
                .AssertException(exception);
        }

        [Theory]
        [InlineData(10d, 11d, BusinessTest.Failure)]
        [InlineData(10d, 10d, BusinessTest.OK)]
        [InlineData(null, 10d, BusinessTest.OK)]
        public void RequiresWhen_Documented(double? withdrawLimit, double withdrawAmount, bool expectedException)
        {
            var exception = expectedException ? $"Withdraw Amount ({withdrawAmount}) exceeds the Withdraw Limit ({withdrawLimit})" : null;

            this.Act(() =>
                    Business.When(withdrawLimit != null)["Withdraw Limit is set"]
                            .Requires(withdrawAmount <= withdrawLimit)["Withdraw Amount must be <= to Withdraw Limit"]
                            .Throws($"Withdraw Amount ({withdrawAmount}) exceeds the Withdraw Limit ({withdrawLimit})")
                )
                .AssertException(exception)
                .AssertRequirementDescription("WHEN Withdraw Limit is set THEN Withdraw Amount must be <= to Withdraw Limit");
        }
        
        [Theory]
        [InlineData(TransactionType.OnlinePayment , 10d, 11d, BusinessTest.Failure)]
        [InlineData(TransactionType.OnlinePayment, 10d, 10d, BusinessTest.OK)]
        [InlineData(TransactionType.OnlinePayment, null, 10d, BusinessTest.OK)]
        [InlineData(TransactionType.ATM , 10d, 10d, BusinessTest.OK)]
        [InlineData(TransactionType.ATM, null, 10d, BusinessTest.OK)]
        public void WhenAndRequires(TransactionType transactionType, double? onlinePaymentLimit, double paymentAmount, bool expectedException)
        {
            var exception = expectedException ? $"Online Payment Amount ({paymentAmount}) exceeds the Online Payment Limit ({onlinePaymentLimit})" : null;

            this.Act(() =>
                    Business.When(transactionType == TransactionType.OnlinePayment)
                            .And(onlinePaymentLimit != null)
                            .Requires(paymentAmount <= onlinePaymentLimit)
                            .Throws($"Online Payment Amount ({paymentAmount}) exceeds the Online Payment Limit ({onlinePaymentLimit})")
                )
                .AssertException(exception);
        }
        
        [Theory]
        [InlineData(TransactionType.OnlinePayment , 10d, 11d, BusinessTest.Failure)]
        [InlineData(TransactionType.OnlinePayment, 10d, 10d, BusinessTest.OK)]
        [InlineData(TransactionType.OnlinePayment, null, 10d, BusinessTest.OK)]
        [InlineData(TransactionType.ATM , 10d, 10d, BusinessTest.OK)]
        [InlineData(TransactionType.ATM, null, 10d, BusinessTest.OK)]
        public void WhenAndRequires_Documented(TransactionType transactionType, double? onlinePaymentLimit, double paymentAmount, bool expectedException)
        {
            var exception = expectedException ? $"Online Payment Amount ({paymentAmount}) exceeds the Online Payment Limit ({onlinePaymentLimit})" : null;

            this.Act(() =>
                    Business.When(transactionType == TransactionType.OnlinePayment)["Transaction is Online Payment"]
                            .And(onlinePaymentLimit != null)["Online Payment Limit is set"]
                            .Requires(paymentAmount <= onlinePaymentLimit)["Online Payment Amount must be <= to Online Payment Limit"]
                            .Throws($"Online Payment Amount ({paymentAmount}) exceeds the Online Payment Limit ({onlinePaymentLimit})")
                )
                .AssertException(exception)
                .AssertRequirementDescription("WHEN Transaction is Online Payment AND Online Payment Limit is set THEN Online Payment Amount must be <= to Online Payment Limit");
        }
        
        protected TestResult Act(Action action)
        {
            try
            {
                action();
                return new TestResult();
            }
            catch (Exception e)
            {
                return new TestResult(e);
            }
        }

        protected class TestResult
        {
            public Exception? Exception { get; }

            public TestResult(Exception exception)
            {
                this.Exception = exception;
            }

            public TestResult()
            {
            }

            public TestResult AssertException(string? expectedException)
            {
                if (expectedException == null)
                    Assert.Null(this.Exception);
                else
                    Assert.Equal(expectedException, this.Exception?.Message);

                return this;
            }

            public void AssertRequirementDescription(string expectedRule)
            {
                var businessException = this.Exception as BusinessRuleViolationException;
                if (businessException == null)
                    return;

                var requirement = businessException.Requirement.ToString();
                
                Assert.Equal(expectedRule, requirement);
            }
        }

        public enum TransactionType
        {
            OnlinePayment,
            ATM
        }
    }
}