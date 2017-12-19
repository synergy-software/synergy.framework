using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using NHibernate;
using Synergy.Contracts;
using Synergy.NHibernate.Engine;
using Synergy.Reflection;

namespace Synergy.NHibernate.Transactions
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class TransactionCoordinator : ITransactionCoordinator
    {
        private readonly IDatabaseProvider databaseProvider;

        /// <summary>
        /// WARN: Component constructor called by Windsor container. DO NOT USE IT DIRECTLY.
        /// </summary>
        public TransactionCoordinator(IDatabaseProvider databaseProvider)
        {
            this.databaseProvider = databaseProvider;
        }

        /// <inheritdoc />
        public TransactionsContainer StartTransactionsFor(MethodInfo method)
        {
            Fail.IfArgumentNull(method, nameof(method));

            List<ConnectToAttribute> transactionAttributes = TransactionCoordinator.GetAutoTransactionAttributesFor(method);

            // Remove all the attributes with disabled transaction
            var enabledTransactions =  transactionAttributes.Where(t => t.Transactional).ToArray();
            TransactionsContainer transactionsContainer = this.AutostartTransactions(enabledTransactions);

            var disabledTransactions = transactionAttributes.Where(t => t.Transactional = false).ToArray();
            this.FailIfTransactionStartedDespiteDisablingIt(transactionsContainer, disabledTransactions);

            return transactionsContainer;
        }

        private void FailIfTransactionStartedDespiteDisablingIt(TransactionsContainer transactionsContainer, [NotNull] ConnectToAttribute[] disabledTransactions)
        {
            Fail.IfArgumentNull(disabledTransactions, nameof(disabledTransactions));

            foreach (ConnectToAttribute disabledTransaction in disabledTransactions)
            {
                IDatabase database = this.GetDatabaseForAutoTransaction(disabledTransaction);
                ISession session = transactionsContainer.StartSession(database);
                Fail.IfTrue(
                    session.Transaction.IsActive,
                    "Transaction is started to database {0} and it shouldn't be due to attribute {1}",
                    database,
                    disabledTransaction);
            }
        }

        [NotNull]
        private TransactionsContainer AutostartTransactions([NotNull] ConnectToAttribute[] enabledTransactions)
        {
            Fail.IfArgumentNull(enabledTransactions, nameof(enabledTransactions));

            var container = new TransactionsContainer();
            foreach (ConnectToAttribute transactionalAttribute in enabledTransactions)
            {
                IDatabase database = this.GetDatabaseForAutoTransaction(transactionalAttribute);
                IsolationLevel isolationLevel = transactionalAttribute.IsolationLevel;

                container.StartTransaction(database, isolationLevel);
            }

            return container;
        }

        [NotNull] 
        private IDatabase GetDatabaseForAutoTransaction([NotNull] ConnectToAttribute transactionalAttribute)
        {
            Fail.IfArgumentNull(transactionalAttribute, nameof(transactionalAttribute));

            Type databaseType = transactionalAttribute.Database;
            Fail.IfNull(databaseType, "There is no database pointed in {0}", transactionalAttribute);

            IDatabase database = this.databaseProvider.Get(databaseType);
            Fail.IfNull(database,
                "Could not find database used in {0}. Did you point {1} in the '{2}' argument?",
                transactionalAttribute,
                nameof(IDatabase),
                nameof(ConnectToAttribute.Database));

            return database;
        }

        [NotNull]
        private static List<ConnectToAttribute> GetAutoTransactionAttributesFor([NotNull] MethodInfo method)
        {
            Fail.IfArgumentNull(method, nameof(method));

            ConnectToAttribute[] transactionsOnMethod = method.GetCustomAttributesBasedOn<ConnectToAttribute>();
            ConnectToAttribute[] transactionsOnClass = method.DeclaringType.OrFail(nameof(MemberInfo.DeclaringType))
                                                                   .GetCustomAttributesBasedOn<ConnectToAttribute>();

            List<ConnectToAttribute> transactionAttributes = transactionsOnMethod.ToList();
            foreach (ConnectToAttribute transactionalAttribute in transactionsOnClass)
                if (transactionAttributes.Any(attr => attr.Database == transactionalAttribute.Database) == false)
                    transactionAttributes.Add(transactionalAttribute);

            return transactionAttributes;
        }
    }

    public interface ITransactionCoordinator
    {
        [NotNull]
        TransactionsContainer StartTransactionsFor([NotNull] MethodInfo method);
    }
}