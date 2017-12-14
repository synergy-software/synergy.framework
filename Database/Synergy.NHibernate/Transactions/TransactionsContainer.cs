using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        private readonly List<SingleTransacion> transactions = new List<SingleTransacion>(1);

        public void StartTransaction([NotNull] IDatabase database, IsolationLevel isolationLevel)
        {
            Fail.IfArgumentNull(database, nameof(database));

            ISession session = database.GetSession();
            bool sessionJustCreated = false;
            if (session == null)
            {
                session = database.OpenSession();
                sessionJustCreated = true;
            }

            ITransaction transaction = session.Transaction;
            bool transactionJustStarted = false;
            if (session.Transaction.IsActive == false)
            {
                transaction = session.BeginTransaction(isolationLevel);
                transactionJustStarted = true;
            }

            this.Add(database, session, transaction, sessionJustCreated, transactionJustStarted);
        }

        /// <summary>
        /// Adds a transaction to this container.
        /// </summary>
        private void Add([NotNull] IDatabase database,
            [NotNull] ISession session,
            [NotNull] ITransaction transaction,
            bool sessionJustCreated,
            bool transactionJustStarted)
        {
            this.transactions.Add(new SingleTransacion
            {
                Database = database,
                Session = session,
                SessionJustCreated = sessionJustCreated,
                Transaction = transaction,
                TransactionJustStarted = transactionJustStarted
            });
        }

        [NotNull, ItemNotNull, Pure]
        private IEnumerable<ITransaction> JustStartedTransactions()
        {
            return this.transactions.Where(t => t.TransactionJustStarted)
                       .Select(t => t.Transaction);
        }

        [NotNull, ItemNotNull, Pure]
        private ISession[] JustStartedSessions()
        {
            return this.transactions.Where(t => t.SessionJustCreated)
                       .Select(t => t.Session)
                       .ToArray();
        }

        /// <summary>
        /// Commits all the transactions in this container.
        /// </summary>
        public void Commit()
        {
            foreach (ITransaction transaction in this.JustStartedTransactions())
                transaction.Commit();
        }

        /// <summary>
        /// Disposes all newly started transaction in this container.
        /// If the transactions were committed via <see cref="Commit"/> method then the disposal does nothing interesting.
        /// If the transactions were not committed, disposing this container will automatically rollback them.
        /// </summary>
        public void Dispose()
        {
            foreach (var transaction in this.JustStartedTransactions())
                transaction.Dispose();

            foreach (ISession session in this.JustStartedSessions())
                session.Dispose();

            this.transactions.Clear();
        }

        private class SingleTransacion
        {
            public IDatabase Database;
            public ISession Session;
            public bool SessionJustCreated;
            public ITransaction Transaction;
            public bool TransactionJustStarted;
        }
    }
}