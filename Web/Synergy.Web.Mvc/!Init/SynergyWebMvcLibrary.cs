using Synergy.Core;
using Synergy.Core.Windsor;
using Synergy.Web.Mvc.Windsor;

namespace Synergy.Web.Mvc
{
    /// <summary>
    ///     Library containig Synergy Framework MVC support.
    /// </summary>
    public class SynergyWebMvcLibrary : Library
    {
        public SynergyWebMvcLibrary()
        {
        }

        /// <inheritdoc />
        public override bool SearchWindsorInstallersInThisAssembly => false;

        /// <inheritdoc />
        public override IWindsorEngineExtension[] GetWindsorEngineExtensions()
        {
            //
            // WARN: When application injects this Library it means that there may be some MVC Controllers around there in the application.
            //       The below extension instructs WindsorEngine to search MVC components (i.e. Controllers) in the application libraries.
            //
            return new IWindsorEngineExtension[] {new MvcWindsorEngineExtension()};
        }
    }
}