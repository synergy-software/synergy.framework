using NUnit.Framework;
using Synergy.Core.Windsor;

namespace Synergy.NHibernate.Test.Transactions
{
    [TestFixture]
    [DatabaseTest]
    public class AutoTransactionTest
    {
        [Test]
        public void automatic_transaction_can_be_disabled()
        {
                //ARRANGE
                IWindsorEngine windsorEngine = ApplicationServer.Start();
                var myService = windsorEngine.GetComponent<IMyTransactionalService>();

                //ACT
                bool transactionStarted = myService.MethodWithDisabledAutoTransaction();

                //ASSERT
                Assert.IsFalse(transactionStarted, "Transaction was started but it shouldn't be");
                windsorEngine.Dispose();
        }

        [Test]
        public void automatic_transaction_should_be_started()
        {
            //ARRANGE
            IWindsorEngine windsorEngine = ApplicationServer.Start();
            var myService = windsorEngine.GetComponent<IMyTransactionalService>();

            //ACT
            int count = myService.GetMyEntitiesCount();

            //ASSERT
            Assert.AreEqual(0, count);
            windsorEngine.Dispose();
        }

        [Test]
        public void automatic_transaction_should_be_started_because_of_attribute_inheritance()
        {
            //ARRANGE
            IWindsorEngine windsorEngine = ApplicationServer.Start();
            var myService = windsorEngine.GetComponent<IMyTransactionalService>();

            //ACT
            int count = myService.StartTransactionBecauseThereIsAttributeOnInterface();

            //ASSERT
            Assert.AreEqual(0, count);
            windsorEngine.Dispose();
        }

        [Test]
        public void can_have_multiple_transactions_to_the_same_database()
        {
            //ARRANGE
            IWindsorEngine windsorEngine = ApplicationServer.Start();
            var myService = windsorEngine.GetComponent<IMyTransactionalService>();

            //ACT
            myService.InvokeAnotherSession();

            //ASSERT
            windsorEngine.Dispose();
        }
    }
}