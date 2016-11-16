using System.Reflection;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;
using Synergy.Contracts;
using Synergy.Core;

namespace Synergy.Web.Mvc.Windsor
{
    /// <summary>
    ///     Installer that registers all MVC controllers in Windsor engine.
    ///     Each controller is available by its name - e.g. "HomeController" is the name.
    ///     Each controller is transient - every request will have a separate instance of the controller.
    /// </summary>
    [UsedImplicitly]
    public class MvcControllerInstaller : IWindsorInstaller
    {
        [NotNull]
        private readonly Library library;

        /// <summary>
        ///     Creates the installer for specified <see cref="Library" />.
        /// </summary>
        public MvcControllerInstaller([NotNull] Library library)
        {
            Fail.IfArgumentNull(library, nameof(library));

            this.library = library;
        }

        /// <summary>
        ///     Installs all MVC controllers form a <see cref="Library" /> in Windsor engine.
        /// </summary>
        public void Install([NotNull] IWindsorContainer container, [NotNull] IConfigurationStore store)
        {
            Fail.IfArgumentNull(container, nameof(container));
            Fail.IfArgumentNull(store, nameof(store));

            Assembly assembly = this.library.GetAssembly();

            BasedOnDescriptor allControllers = Classes
                .FromAssembly(assembly)
                .BasedOn<Controller>()
                .WithServiceSelf()
                .Configure(c => c.Named(c.Implementation.Name)
                                 .LifestyleTransient()
                );

            container.Register(allControllers);
        }
    }
}