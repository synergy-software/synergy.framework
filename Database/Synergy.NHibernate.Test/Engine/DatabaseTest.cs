using NHibernate;
using NUnit.Framework;
using Synergy.Core.Windsor;
using Synergy.NHibernate.Test.My;

namespace Synergy.NHibernate.Test.Engine
{
    [TestFixture]
    public class DatabaseTest
    {
        [Test]
        public void database_can_be_opened()
        {
            // ARRANGE
            var rootLibrary = new SynergyNHibernateTestLibrary();
            IWindsorEngine windsorEngine = new WindsorEngine();
            windsorEngine.Start(rootLibrary);
            var db = windsorEngine.GetComponent<IMyDatabase>();

            // ACT
            ISession session = db.OpenSession();

            // ASSERT
            session.Dispose();
            windsorEngine.Dispose();
        }
    }
}