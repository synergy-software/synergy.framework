using System;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using JetBrains.Annotations;
using Synergy.Contracts;
using Synergy.Core.Libraries;

namespace Synergy.Core.Windsor
{
    /// <inheritdoc />
    public class WindsorEngine : IWindsorEngine
    {
        [CanBeNull]
        private WindsorContainer container;

        /// <inheritdoc />
        public void Start(Library rootLibrary)
        {
            Fail.IfArgumentNull(rootLibrary, nameof(rootLibrary));
            Fail.IfNotNull(this.container, $"{nameof(WindsorEngine)} already started");

            var librarian = new Librarian(rootLibrary);
            Library[] allLibraries = librarian.GetLibraries();
            var extensions = allLibraries.SelectMany(library => library.GetWindsorEngineExtensions()).ToArray();

            this.container = new WindsorContainer();

            this.container.Kernel.Resolver.AddSubResolver(new ComponentCollectionResolver(this.container.Kernel));

            this.container.Register(Component.For<IWindsorContainer>()
                                             .Instance(this.container));
            this.container.Register(Component.For<IWindsorEngine>()
                                             .Instance(this));
            this.container.Register(Component.For<ILibrarian>()
                                             .Instance(librarian));

            foreach (Library library in allLibraries)
                this.RegisterComponentsFrom(library, extensions);
        }

        /// <inheritdoc />
        public T GetComponent<T>()
        {
            return this.GetContainerOrFail()
                       .Resolve<T>();
        }

        /// <inheritdoc />
        public object GetComponent(Type type)
        {
            Fail.IfArgumentNull(type, nameof(type));

            return this.GetContainerOrFail()
                       .Resolve(type);
        }

        /// <inheritdoc />
        public T GetComponent<T>(string name)
        {
            Fail.IfArgumentWhiteSpace(name, nameof(name));

            return this.GetContainerOrFail()
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

            return this.GetContainerOrFail()
                       .Kernel
                       .HasComponent(type);
        }

        /// <inheritdoc />
        public bool HasComponent(string name)
        {
            Fail.IfArgumentWhiteSpace(name, nameof(name));

            return this.GetContainerOrFail()
                       .Kernel
                       .HasComponent(name);
        }

        /// <inheritdoc />
        public void ReleaseComponent(object component)
        {
            Fail.IfArgumentNull(component, nameof(component));

            this.GetContainerOrFail().Release(component);
        }

        /// <inheritdoc />
        public T[] GetComponents<T>()
        {
            return this.GetContainerOrFail()
                       .ResolveAll<T>();
        }

        /// <inheritdoc />
        public void Stop()
        {
            this.GetContainerOrFail();
            this.Dispose(true);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void RegisterComponentsFrom([NotNull] Library library, [NotNull] IWindsorEngineExtension[] extensions)
        {
            Fail.IfArgumentNull(library, nameof(library));
            Fail.IfArgumentNull(extensions, nameof(extensions));

            WindsorContainer windsorContainer = this.GetContainerOrFail();
            IWindsorInstaller libraryInstaller = library.GetWindsorInstaller();
            windsorContainer.Install(libraryInstaller);

            foreach (IWindsorEngineExtension extension in extensions)
            {
                extension.RegisterComponentsFrom(windsorContainer, library);
            }
        }

        private WindsorContainer GetContainerOrFail()
        {
            Fail.IfNull(this.container, $"{nameof(WindsorEngine)} is not started");

            return this.container;
        }

        /// <inheritdoc />
        protected virtual void Dispose(bool disposing)
        {
            if (disposing == false)
                return;

            if (this.container == null)
                return;

            this.container.Dispose();
            this.container = null;
        }
    }

    /// <summary>
    /// Inversion of control engine providing system components instantiation and resolving dependencies between them.
    /// </summary>
    public interface IWindsorEngine : IDisposable
    {
        /// <summary>
        /// Starts a Windsor engine populating it with components from libraries.
        /// First library to scan is the one provided to the method. Next libraries are taken from dependencies of this one.
        /// </summary>
        void Start([NotNull] Library rootLibrary);

        /// <summary>
        /// Stops the engine and releases all components instantiated within the container.
        /// </summary>
        void Stop();

        [NotNull]
        [Pure]
        T GetComponent<T>();

        // TODO:mace (from:mace on:19-11-2016) move all those fancy GetComponent methods to IComponentLocator

        [NotNull]
        [Pure]
        object GetComponent([NotNull] Type type);

        [NotNull]
        [Pure]
        T GetComponent<T>([NotNull] string name);

        [Pure]
        bool HasComponent<T>();

        [Pure]
        bool HasComponent([NotNull] Type type);

        [Pure]
        bool HasComponent([NotNull] string name);

        void ReleaseComponent([NotNull] object component);

        /// <summary>
        /// Gets all components valid for provided type.
        /// </summary>
        [NotNull]
        [Pure]
        T[] GetComponents<T>();
    }
}