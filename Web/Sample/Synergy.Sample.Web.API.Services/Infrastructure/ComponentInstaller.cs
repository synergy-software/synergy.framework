using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;
using Synergy.Sample.Web.API.Services.Infrastructure.Commands;
using Synergy.Sample.Web.API.Services.Infrastructure.Queries;

namespace Synergy.Sample.Web.API.Services.Infrastructure
{
    [UsedImplicitly]
    public class ComponentInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IQueryHandlerFactory>().AsFactory());
            container.Register(Component.For<ICommandHandlerFactory>().AsFactory());

        }
    }
}