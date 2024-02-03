using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using JetBrains.Annotations;
using Synergy.Contracts;

namespace Synergy.Core.Windsor
{
    /// <summary>
    ///     Collection resolver for injecting dependencies of IComponent[] or IEnumerable&lt;IComponent>.
    ///     The injected array of components is wiped up - it means that there are no two component classes that inherit from
    ///     one another. If there is no class implementing the requested interface - an empty array will be injected,
    ///     indicating there is no one interesting around here.
    /// </summary>
    public class ComponentCollectionResolver : CollectionResolver
    {
        /// <summary>
        /// Creates the collection resolver binding it to the specified Castle kernel.
        /// </summary>
        public ComponentCollectionResolver(IKernel kernel) : base(kernel, true)
        {
        }

        [NotNull]
        public override object Resolve(
            [NotNull] CreationContext context,
            [NotNull] ISubDependencyResolver contextHandlerResolver,
            [NotNull] ComponentModel model,
            [NotNull] DependencyModel dependency)
        {
            var array = base.Resolve(context, contextHandlerResolver, model, dependency)
                            .CastOrFail<Array>("array");

            return this.RemoveBaseComponents(array, dependency);
        }

        [NotNull, Pure]
        private Array RemoveBaseComponents([NotNull] Array components, [NotNull] DependencyModel dependency)
        {
            Fail.IfArgumentNull(components, nameof(components));
            Fail.IfArgumentNull(dependency, nameof(dependency));

            var wipedUpComponents = new List<object>(components.Length);
            var types = components
                .Cast<object>()
                .Select(c => c.GetType())
                .ToArray();

            foreach (object component in components)
            {
                Type componentType = component.GetType();

                if (types.Any(type => componentType.IsAssignableFrom(type) && (componentType != type)))
                    continue;

                wipedUpComponents.Add(component);
            }

            if (components.Length == wipedUpComponents.Count)
                return components;

            Type service = this.GetItemType(dependency.TargetItemType);
            Array instance = Array.CreateInstance(service, wipedUpComponents.Count);
            ((ICollection) wipedUpComponents).CopyTo(instance, 0);
            return instance;
        }
    }
}