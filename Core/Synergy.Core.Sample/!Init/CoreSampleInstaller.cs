using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;
using Synergy.Core.Sample.Users;

namespace Synergy.Core.Sample
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class CoreSampleInstaller : IWindsorInstaller
    {
        public void Install([NotNull] IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IUserRepository>()
                                        .ImplementedBy<UserRepository>());
        }
    }
}