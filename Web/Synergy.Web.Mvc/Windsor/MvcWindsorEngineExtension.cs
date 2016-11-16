using Castle.Windsor;
using JetBrains.Annotations;
using Synergy.Contracts;
using Synergy.Core;
using Synergy.Core.Windsor;

namespace Synergy.Web.Mvc.Windsor
{
    /// <summary>
    /// Extension to<see cref="WindsorEngine"/> that installs all MVC components from a <see cref="Library"/>.
    /// It mainly installs all MVC controllers using <see cref="MvcControllerInstaller"/>.
    /// </summary>
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class MvcWindsorEngineExtension : IWindsorEngineExtension
    {
        /// <inheritdoc />
        public void RegisterComponentsFrom(WindsorContainer windsorContainer, Library library)
        {
            Fail.IfArgumentNull(windsorContainer, nameof(windsorContainer));
            Fail.IfArgumentNull(library, nameof(library));

            var controllerInstaller = new MvcControllerInstaller(library);
            windsorContainer.Install(controllerInstaller);
        }
    }
}