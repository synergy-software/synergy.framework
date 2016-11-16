using System.Data;
using Castle.Core;
using JetBrains.Annotations;
using Synergy.Contracts;
using Synergy.NHibernate.Session;
using Synergy.NHibernate.Test.My;
using Synergy.NHibernate.Transactions;

namespace Synergy.NHibernate.Test.Transactions
{
    [Interceptor(typeof(ISessionInterceptor))]
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class MyTransactionalService : IMyTransactionalService
    {
        private readonly IMyDatabase myDatabase;
        private readonly IMyRepository myRepository;

        public MyTransactionalService(IMyRepository myRepository, IMyDatabase myDatabase)
        {
            this.myRepository = myRepository;
            this.myDatabase = myDatabase;
        }

        public int StartTransactionBecauseThereIsAttributeOnInterface()
        {
            Fail.IfFalse(this.myDatabase.CurrentSession.Transaction.IsActive, "Transaction not started");

            return this.myRepository.GetAll()
                       .Length;
        }

        public int GetMyEntitiesCount()
        {
            Fail.IfFalse(this.myDatabase.CurrentSession.Transaction.IsActive, "Transaction not started");

            return this.myRepository.GetAll()
                       .Length;
        }

        [AutoTransaction(On = typeof(IMyDatabase), Disabled = true)]
        public bool MethodWithDisabledAutoTransaction()
        {
            return this.myDatabase.CurrentSession.Transaction.IsActive;
        }
    }

    [AutoTransaction(On = typeof(IMyDatabase), IsolationLevel = IsolationLevel.Serializable)]
    public interface IMyTransactionalService
    {
        [Pure]
        int StartTransactionBecauseThereIsAttributeOnInterface();

        [Pure]
        [AutoTransaction(On = typeof(IMyDatabase), IsolationLevel = IsolationLevel.ReadCommitted)]
        int GetMyEntitiesCount();

        [Pure]
        bool MethodWithDisabledAutoTransaction();
    }
}