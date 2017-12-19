using System.Reflection;
using Castle.Core;
using Castle.DynamicProxy;
using JetBrains.Annotations;
using Synergy.Contracts;
using Synergy.NHibernate.Transactions;
using IInterceptor = Castle.DynamicProxy.IInterceptor;

namespace Synergy.NHibernate.Session
{
    // TODO:mace (from:mace on:07-12-2017) ten interceptor powinien zawsze łączyć się do jakiejś DB - [ConnectTo(typeof(Idatabase), AutoTransaction = true, IsoLevel=)]
    [Transient]
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class SessionInterceptor : ISessionInterceptor
    {
        private readonly ITransactionCoordinator transactionCoordinator;

        /// <summary>
        /// WARN: Component constructor called by Windsor container. DO NOT USE IT DIRECTLY.
        /// </summary>
        public SessionInterceptor(ITransactionCoordinator transactionCoordinator)
        {
            this.transactionCoordinator = transactionCoordinator;
        }

        /// <inheritdoc />
        public void Intercept([NotNull] IInvocation invocation)
        {
            Fail.IfArgumentNull(invocation, nameof(invocation));

            using (var transactions = this.StartTransactions(invocation))
            {
                invocation.Proceed();
                transactions.Commit();
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