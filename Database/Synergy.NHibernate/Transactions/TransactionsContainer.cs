using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NHibernate;
using Synergy.Contracts;
using Synergy.NHibernate.Engine;

namespace Synergy.NHibernate.Transactions
{
    /// <summary>
    /// Container for keeping bunch of transactions for awhile.
    /// When the object is disposed all transactions are also disposed. If <see cref="Commit"/> was called before the disposal 
    /// all the transactions will be committed otherwise everything is rolled back.
    /// </summary>
    public class TransactionsContainer : IDisposable
    {
        private readonly List<ITransaction> transactions = new List<ITransaction>(1);

        /// <summary>
        /// Adds a transaction to this container.
        /// </summary>
        public void Add([NotNull] IDatabase database, [NotNull] ISession session, [NotNull] ITransaction transaction)
        {
            Fail.IfArgumentNull(database, nameof(database));
            Fail.IfArgumentNull(session, nameof(session));
            Fail.IfArgumentNull(transaction, nameof(transaction));

            this.transactions.Add(transaction);
        }

        /// <summary>
        /// Commits all the transactions in this container.
        /// </summary>
        public void Commit()
        {
            foreach (ITransaction transaction in this.transactions)
                transaction.Commit();
        }

        /// <summary>
        /// Disposes all transaction in this container.
        /// If the transactions were committed via <see cref="Commit"/> method then the disposal does nothing interesting.
        /// If the transactions were not committed, disposing this container will automatically rollback them.
        /// </summary>
        public void Dispose()
        {
            foreach (ITransaction transaction in this.transactions)
                transaction.Dispose();

            this.transactions.Clear();
        }
    }
}