using System.Data;
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
    }

    [AutoTransaction(On = typeof(ISampleDatabase), IsolationLevel = IsolationLevel.Serializable)]
    public interface IHomeService
    {
        void CreateDatabaseSchema();
    }
}