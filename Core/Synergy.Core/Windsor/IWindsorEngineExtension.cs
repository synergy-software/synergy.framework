using Castle.Windsor;
using JetBrains.Annotations;

namespace Synergy.Core.Windsor
{
    /// <summary>
    ///     Component extending <see cref="IWindsorEngine" />. Implement it and return it from
    ///     <see cref="Library.GetWindsorEngineExtensions" /> method.
    /// </summary>
    public interface IWindsorEngineExtension
    {
        /// <summary>
        ///     Allows you to register additional components for each <see cref="Library" /> in the application.
        /// </summary>
        void RegisterComponentsFrom([NotNull] WindsorContainer windsorContainer, [NotNull] Library library);
    }
}