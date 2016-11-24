using NUnit.Framework;
using Synergy.Core.Windsor;
using Synergy.NHibernate.Session;

namespace Synergy.NHibernate.Test.Transactions
{
    [TestFixture]
    public class AutoTransactionTest
    {
        [Test]
        public void automatic_transaction_can_be_disabled()
        {
            using (new SessionThreadStaticScope())
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
    }
}