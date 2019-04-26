using JetBrains.Annotations;
using Synergy.Core.Windsor;
using Synergy.NHibernate.Test.Database.Schema;
using Synergy.NHibernate.Test.My;

namespace Synergy.NHibernate.Test
{
    public class ApplicationServer
    {
        [NotNull]
        public static IWindsorEngine Start()
        {
            var rootLibrary = new SynergyNHibernateTestLibrary();
            IWindsorEngine windsorEngine = new WindsorEngine();
            windsorEngine.Start(rootLibrary);

            var db = windsorEngine.GetComponent<IMyDatabase>();
            db.Open();
            //using (db.OpenSession())
            {
                db.OpenSession();
                var schema = windsorEngine.GetComponent<IDatabaseSchema>();
                schema.CreateFor(db);
            }


            return windsorEngine;
        }
    }
}
