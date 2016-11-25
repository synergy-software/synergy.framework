using System.Reflection;
using Castle.Core;
using Castle.Core.Internal;
using Castle.DynamicProxy;
using JetBrains.Annotations;
using Synergy.Contracts;
using Synergy.NHibernate.Transactions;
using IInterceptor = Castle.DynamicProxy.IInterceptor;

namespace Synergy.NHibernate.Session
{
    [Transient]
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class SessionInterceptor : ISessionInterceptor
    {
        private readonly ISessionContext sessionContext;
        private readonly ITransactionCoordinator transactionCoordinator;

        /// <summary>
        /// WARN: Component constructor called by Windsor container. DO NOT USE IT DIRECTLY.
        /// </summary>
        public SessionInterceptor(ISessionContext sessionContext, ITransactionCoordinator transactionCoordinator)
        {
            this.sessionContext = sessionContext;
            this.transactionCoordinator = transactionCoordinator;
        }

        public void Intercept([NotNull] IInvocation invocation)
        {
            Fail.IfArgumentNull(invocation, nameof(invocation));

            try
            {
                using (var transactions = this.StartTransactions(invocation))
                {
                    invocation.Proceed();
                    transactions.Commit();
                }
            }
            finally
            {
                var sessions = this.sessionContext.RemoveSessions();
                sessions.ForEach(s => s.Dispose());
            }
        }

        [NotNull, Pure]
        private TransactionsContainer StartTransactions([NotNull] IInvocation invocation)
        {
            MethodInfo method = invocation.GetConcreteMethodInvocationTarget();
            return this.transactionCoordinator.StartTransactionsFor(method);
        }
    }

    public interface ISessionInterceptor : IInterceptor
    {
    }
}