using System;
using Castle.Windsor;
using JetBrains.Annotations;
using Synergy.Contracts;

// ReSharper disable once CheckNamespace
namespace Synergy.Core
{
    /// <inheritdoc />
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class ComponentLocator : IComponentLocator
    {
        private readonly IWindsorContainer windsorContainer;

        public ComponentLocator(IWindsorContainer windsorContainer)
        {
            this.windsorContainer = windsorContainer;
        }

        /// <inheritdoc />
        public T GetComponent<T>()
        {
            return this.windsorContainer.Resolve<T>();
        }

        /// <inheritdoc />
        public object GetComponent(Type type)
        {
            Fail.IfArgumentNull(type, nameof(type));

            return this.windsorContainer.Resolve(type);
        }

        /// <inheritdoc />
        public T GetComponent<T>(string name)
        {
            Fail.IfArgumentWhiteSpace(name, nameof(name));

            return this.windsorContainer
                       .Kernel
                       .Resolve<T>(name);
        }

        /// <inheritdoc />
        public bool HasComponent<T>()
        {
            return this.HasComponent(typeof(T));
        }

        /// <inheritdoc />
        public bool HasComponent(Type type)
        {
            Fail.IfArgumentNull(type, nameof(type));

            return this.windsorContainer
                       .Kernel
                       .HasComponent(type);
        }

        /// <inheritdoc />
        public bool HasComponent(string name)
        {
            Fail.IfArgumentWhiteSpace(name, nameof(name));

            return this.windsorContainer
                       .Kernel
                       .HasComponent(name);
        }

        /// <inheritdoc />
        public void ReleaseComponent(object component)
        {
            Fail.IfArgumentNull(component, nameof(component));

            this.windsorContainer.Release(component);
        }

        /// <inheritdoc />
        public T[] GetComponents<T>()
        {
            return this.windsorContainer
                       .ResolveAll<T>();
        }

        /// <inheritdoc />
        public object[] GetComponents(Type type)
        {
            return (object[]) this.windsorContainer
                                  .ResolveAll(type);
        }
    }

    public interface IComponentLocator
    {
        /// <summary>
        ///     Gets component valid for provided type.
        /// </summary>
        [NotNull, Pure]
        T GetComponent<T>();

        [NotNull, Pure]
        object GetComponent([NotNull] Type type);

        [NotNull, Pure]
        T GetComponent<T>([NotNull] string name);

        [Pure]
        bool HasComponent<T>();

        [Pure]
        bool HasComponent([NotNull] Type type);

        [Pure]
        bool HasComponent([NotNull] string name);

        void ReleaseComponent([NotNull] object component);

        /// <summary>
        ///     Gets all components valid for provided type.
        /// </summary>
        [NotNull, Pure]
        T[] GetComponents<T>();

        /// <summary>
        ///     Gets all components valid for provided type.
        /// </summary>
        [NotNull, Pure]
        object[] GetComponents([NotNull] Type type);
    }
}