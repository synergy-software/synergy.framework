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

        [NotNull]
        public WindsorContainer Container => this.GetContainerOrFail();

        /// <inheritdoc />
        public void Start(Library rootLibrary)
        {
            Fail.IfArgumentNull(rootLibrary, nameof(rootLibrary));
            Fail.IfNotNull(this.container, Violation.Message($"{nameof(WindsorEngine)} already started"));

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
        public void Stop()
        {
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

        [NotNull, Pure]
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
    }
}