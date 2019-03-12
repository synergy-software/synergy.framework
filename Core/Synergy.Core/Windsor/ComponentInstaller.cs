using System;
using System.Linq;
using System.Reflection;
using Castle.Core;
using Castle.DynamicProxy.Internal;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;
using Synergy.Contracts;
using Synergy.Core.Extensions;

namespace Synergy.Core.Windsor
{
    /// <summary>
    ///     Default Windsor installer. It searches for any component with any interface in a <see cref="Library" />
    ///     and registers the component under all interfaces it implements.
    /// </summary>
    public class ComponentInstaller : IWindsorInstaller
    {
        [CanBeNull]
        private Assembly assembly;

        [CanBeNull]
        private Type[] excludeInterfaces;

        /// <summary>
        /// Initialize the installer with <see cref="Library" /> that will be searched for components.
        /// </summary>
        public ComponentInstaller([NotNull] Library library)
        {
            this.Init(library);
        }

        private void Init([NotNull] Library library)
        {
            Fail.IfArgumentNull(library, nameof(library));

            this.assembly = library
                .GetAssembly()
                .OrFail(nameof(this.assembly));

            this.excludeInterfaces = library
                .IgnoreInterfaces()
                .OrFail(nameof(this.excludeInterfaces))
                .ToArray();
        }

        /// <summary>
        /// Installs any component with any interface in a <see cref="Library" /> and registers the component under all interfaces it implements.
        /// </summary>
        public void Install([NotNull] IWindsorContainer container, [NotNull] IConfigurationStore store)
        {
            Fail.IfArgumentNull(container, nameof(container));

            Assembly assemblyToScan = this.assembly.OrFail(nameof(this.assembly));

            BasedOnDescriptor allClassesWithAnyInterface = Classes
                    .FromAssembly(assemblyToScan)
                    .Pick()
                    .If(this.ShouldRegisterComponent)
                    .WithService.Select(this.GetComponentInterfaces())
                    .Configure(this.ConfigureComponent)
                ;

            container.Register(allClassesWithAnyInterface);
        }

        private void ConfigureComponent([NotNull] ComponentRegistration obj)
        {
            if (obj.Implementation.HasCustomAttribute<SingletonAttribute>())
                obj.LifestyleSingleton();

            //obj.LifestyleTransient();
        }

        [Pure]
        private bool ShouldRegisterComponent([NotNull] Type componentType)
        {
            Fail.IfArgumentNull(componentType, nameof(componentType));

            Type[] interfacesToExclude = this.excludeInterfaces.OrFail(nameof(this.excludeInterfaces));

            Type[] interfaces = componentType
                .GetInterfaces()
                .Except(interfacesToExclude)
                .ToArray();

            return interfaces.Any();
        }

        [NotNull]
        private ServiceDescriptor.ServiceSelector GetComponentInterfaces()
        {
            Type[] interfacesToExclude = this.excludeInterfaces.OrFail(nameof(this.excludeInterfaces));

            return (componentType, basTypes) => componentType
                .GetAllInterfaces()
                .Except(interfacesToExclude);
        }
    }
}