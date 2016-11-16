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

        public void Add(IDatabase database, ISession session, [NotNull] ITransaction transaction)
        {
            Fail.IfArgumentNull(transaction, nameof(transaction));

            this.transactions.Add(transaction);
        }

        public void Commit()
        {
            foreach (ITransaction transaction in this.transactions)
                transaction.Commit();
        }

        public void Dispose()
        {
            foreach (ITransaction transaction in this.transactions)
                transaction.Dispose();

            this.transactions.Clear();
        }
    }
}