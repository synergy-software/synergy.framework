using Castle.Core;
using JetBrains.Annotations;

namespace Synergy.Core.Test.Interceptors
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    [Interceptor(typeof(IComponentInterceptor))]
    public class InterceptedComponent : IInterceptedComponent
    {
        public void Execute()
        {
        }
    }

    public interface IInterceptedComponent
    {
        void Execute();
    }
}