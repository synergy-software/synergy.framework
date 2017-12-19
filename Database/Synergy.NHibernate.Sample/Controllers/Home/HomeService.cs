using System.Data;
using System.Threading.Tasks;
using Castle.Core;
using JetBrains.Annotations;
using Synergy.Contracts;
using Synergy.NHibernate.Sample.Domain;
using Synergy.NHibernate.Sample.Domain.Schema;
using Synergy.NHibernate.Session;
using Synergy.NHibernate.Transactions;

namespace Synergy.NHibernate.Sample.Controllers.Home
{
    [Interceptor(typeof(ISessionInterceptor))]
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class HomeService : IHomeService
    {
        private readonly IDatabaseSchema dataBaseSchema;
        private readonly ISampleDatabase sampleDatabase;

        public HomeService(ISampleDatabase sampleDatabase, IDatabaseSchema dataBaseSchema)
        {
            this.sampleDatabase = sampleDatabase;
            this.dataBaseSchema = dataBaseSchema;
        }

        public void CreateDatabaseSchema()
        {
            Fail.IfFalse(this.sampleDatabase.CurrentSession.Transaction.IsActive, "Transaction not started");

            this.dataBaseSchema.CreateFor(this.sampleDatabase);
        }

        public void InvokeAnotherSession()
        {
            Fail.IfFalse(this.sampleDatabase.CurrentSession.Transaction.IsActive, "Transaction not started");

            var task = new Task(session =>
            {
                var currentSession = this.sampleDatabase.CurrentSession;
                Fail.IfEqual(session, currentSession, "session should differ");

            }, this.sampleDatabase.CurrentSession);
            task.Start();
            task.Wait();
        }
    }

    [ConnectTo(typeof(ISampleDatabase), Transactional = true, IsolationLevel = IsolationLevel.Serializable)]
    public interface IHomeService
    {
        void CreateDatabaseSchema();
        void InvokeAnotherSession();
    }
}