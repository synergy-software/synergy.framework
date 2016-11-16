using Castle.Core;
using Castle.DynamicProxy;
using JetBrains.Annotations;
using Synergy.Contracts;

namespace Synergy.Core.Test.Interceptors
{
    [Transient]
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class ComponentInterceptor : IComponentInterceptor
    {
        public static bool WasInvoked;

        public void Intercept([NotNull] IInvocation invocation)
        {
            Fail.IfArgumentNull(invocation, nameof(invocation));

            ComponentInterceptor.WasInvoked = true;
            invocation.Proceed();
        }
    }

    internal interface IComponentInterceptor : IInterceptor
    {
    }
}