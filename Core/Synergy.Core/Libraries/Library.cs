using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.Serialization;
using Castle.MicroKernel.Registration;
using Castle.Windsor.Installer;
using JetBrains.Annotations;
using Synergy.Contracts;
using Synergy.Core.Windsor;

// ReSharper disable once CheckNamespace

namespace Synergy.Core
{
    /// <summary>
    ///     Base class for application Libraries. Inherit from it to have a starting point of your
    ///     <see cref="Assembly" /> / <see cref="Library" />.
    /// </summary>
    [DebuggerDisplay("{" + nameof(Library.DebuggerDisplay) + ",nq}")]
    public abstract class Library : IEquatable<Library>
    {
        /// <summary>
        ///     Allows you to provide your library dependencies - libraries used by your library.
        /// </summary>
        protected Library([NotNull] params Library[] dependencies)
        {
            Fail.IfArgumentNull(dependencies, nameof(dependencies));

            this.Dependencies = dependencies;
        }

        /// <summary>
        ///     Gets dependent Libraries of this <see cref="Library" />.
        /// </summary>
        [NotNull]
        public Library[] Dependencies { get; }

        /// <summary>
        ///     Determines whether <see cref="WindsorEngine" /> should automatically populate all components from this
        ///     <see cref="Library" />.
        /// </summary>
        [PublicAPI]
        public virtual bool PopulateComponentsAutomatically => true;

        /// <summary>
        ///     Determines whether <see cref="WindsorEngine" /> should search for installers declared in the assembly represented
        ///     by this <see cref="Library" />.
        /// </summary>
        [PublicAPI]
        public virtual bool SearchWindsorInstallersInThisAssembly => true;

        [NotNull]
        [ExcludeFromCodeCoverage]
        private string DebuggerDisplay => $"{this} -> {this.GetAssembly().GetName().Name}.dll";

        /// <inheritdoc />
        public bool Equals(Library other)
        {
            Fail.IfArgumentNull(other, nameof(other));

            return this.GetAssembly() == other.GetAssembly();
        }

        /// <summary>
        ///     Determines whether the specified object is equal to the current object.
        /// </summary>
        [Pure]
        public override bool Equals(object obj)
        {
            var otherLibrary = obj as Library;
            if (otherLibrary != null)
                return this.Equals(otherLibrary);
            return false;
        }

        /// <summary>
        ///     Returns the hash code for this instance.
        /// </summary>
        [Pure]
        public override int GetHashCode()
        {
            return this.GetAssembly()
                       .OrFail(nameof(this.GetAssembly))
                       .GetHashCode();
        }

        /// <summary>
        ///     Returns <see cref="Assembly" /> represented by the <see cref="Library" />.
        ///     <para>
        ///         Override it when the <see cref="Library" /> is not implemented inside the <see cref="Assembly" /> it
        ///         represents.
        ///     </para>
        /// </summary>
        [NotNull]
        [Pure]
        // ReSharper disable once VirtualMemberNeverOverridden.Global
        public virtual Assembly GetAssembly()
        {
            return this.GetType()
                       .Assembly;
        }

        /// <summary>
        ///     Returns <see cref="IWindsorInstaller" /> responsible for installation of all required components from the Library.
        /// </summary>
        [NotNull]
        [Pure]
        // ReSharper disable once VirtualMemberNeverOverridden.Global
        public virtual IWindsorInstaller GetWindsorInstaller()
        {
            Assembly assembly = this.GetAssembly();

            var compositeInstaller = new CompositeInstaller();

            if (this.SearchWindsorInstallersInThisAssembly)
            {
                IWindsorInstaller assemblySpecificInstallers = FromAssembly.Instance(assembly);
                compositeInstaller.Add(assemblySpecificInstallers);
            }

            if (this.PopulateComponentsAutomatically)
            {
                var componentInstaller = new ComponentInstaller(this);
                compositeInstaller.Add(componentInstaller);
            }

            return compositeInstaller;
        }

        /// <summary>
        ///     Returns interfaces that should be ignored when searching components in the <see cref="Library" />.
        ///     <para>Override it to ignore interfaces you do NOT want to be components.</para>
        /// </summary>
        [NotNull]
        [Pure]
        // ReSharper disable once VirtualMemberNeverOverridden.Global
        public virtual IEnumerable<Type> IgnoreInterfaces()
        {
            return new[]
            {
#if !NETSTANDARD2_0
                typeof(System.Runtime.InteropServices._Attribute),
                typeof(System.Runtime.InteropServices._Exception),
#endif
                typeof(ISerializable),
                typeof(IWindsorInstaller),
                typeof(IDisposable)
            };
        }

        /// <summary>
        /// Gets an object allowing to extend Widnsor component registration for each Library in the system.
        /// </summary>
        [NotNull]
        [ItemNotNull]
        [Pure]
        // ReSharper disable once VirtualMemberNeverOverridden.Global
        public virtual IWindsorEngineExtension[] GetWindsorEngineExtensions()
        {
            return new IWindsorEngineExtension[0];
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.GetAssembly().ToString();
        }
    }
}