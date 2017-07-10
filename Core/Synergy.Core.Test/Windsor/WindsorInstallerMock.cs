using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;

namespace Synergy.Core.Test.Windsor
{
    [UsedImplicitly]
    public class WindsorInstallerMock : IWindsorInstaller
    {
        public void Install([NotNull] IWindsorContainer container, [NotNull] IConfigurationStore store)
        {
            // see: https://github.com/castleproject/Windsor/blob/master/docs/registering-components-one-by-one.md
            container
                .Register(
                    Component
                        .For<IComponentCreatedViaFactory>()
                        .LifestyleTransient()
                        .UsingFactoryMethod(this.CreateMyService)
                );
        }

        [NotNull]
        private IComponentCreatedViaFactory CreateMyService()
        {
            return new ComponentCreatedViaFactory();
        }

        private class ComponentCreatedViaFactory : IComponentCreatedViaFactory
        {
        }
    }

    public interface IComponentCreatedViaFactory
    {
    }

    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    [Singleton]
    public class ContainerForComponentCreatedViaFactory : IContainerForComponentCreatedViaFactory
    {
        private readonly IComponentCreatedViaFactory componentCreatedViaFactory;

        public ContainerForComponentCreatedViaFactory(IComponentCreatedViaFactory componentCreatedViaFactory)
        {
            this.componentCreatedViaFactory = componentCreatedViaFactory;
        }

        public IComponentCreatedViaFactory GetComponentCreatedViaFactory()
        {
            return this.componentCreatedViaFactory;
        }
    }

    public interface IContainerForComponentCreatedViaFactory
    {
        IComponentCreatedViaFactory GetComponentCreatedViaFactory();
    }
}