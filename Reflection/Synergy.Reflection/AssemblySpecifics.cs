using System.Diagnostics;
using System.Reflection;
using JetBrains.Annotations;
using Synergy.Contracts;

namespace Synergy.Reflection
{
    /// <summary>
    /// Helper class for reading <see cref="Assembly"/> metadata.
    /// It is a reflection wrapper.
    /// </summary>
    [DebuggerDisplay("{" + nameof(AssemblySpecifics.DebuggerDisplay) + ",nq}")]
    public class AssemblySpecifics
    {
        private readonly Assembly assembly;

        private AssemblySpecifics([NotNull] Assembly assembly)
        {
            Fail.IfArgumentNull(assembly, nameof(assembly));

            this.assembly = assembly;
        }

        [NotNull, Pure]
        public static AssemblySpecifics Of<T>()
        {
            return AssemblySpecifics.Of(typeof(T).Assembly);
        }

        [NotNull, Pure]
        public static AssemblySpecifics Of([NotNull] Assembly assembly)
        {
            Fail.IfArgumentNull(assembly, nameof(assembly));

            return new AssemblySpecifics(assembly);
        }

        /// <summary>
        /// Gets the <see cref="Assembly"/> version in "MAJOR.MINOR.BUILD.REVISION" format.
        /// </summary>
        [NotNull, Pure]
        public string GetVersion()
        {
            return this.assembly
                .GetName()
                .Version
                .ToString();
        }

        /// <summary>
        /// Gets the <see cref="Assembly"/> file version compiled into <see cref="AssemblyFileVersionAttribute"/>.
        /// It returns null when there is no such attribute in the <see cref="Assembly"/>.
        /// </summary>
        [CanBeNull, Pure]
        public string GetFileVersion()
        {
            return this.assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()
                ?.Version.FailIfNull(Violation.Of("{1} is empty in assembly '{0}'", nameof(AssemblyFileVersionAttribute), this.assembly.FullName));
        }

        /// <summary>
        /// Gets the <see cref="Assembly"/> product name compiled into <see cref="AssemblyProductAttribute"/>.
        /// It returns null when there is no such attribute in the <see cref="Assembly"/>.
        /// </summary>
        [CanBeNull, Pure]
        public string GetProduct()
        {
            return this.assembly.GetCustomAttribute<AssemblyProductAttribute>()
                ?.Product.FailIfNull(Violation.Of("{1} is empty in assembly '{0}'", nameof(AssemblyProductAttribute), this.assembly.FullName));
        }

        //[NotNull, Pure]
        //private T GetCustomAttribute<T>() where T : Attribute
        //{
        //    var customAttribute = this.assembly.GetCustomAttribute<T>();
        //    Fail.IfNull(customAttribute, "Assembly '{0}' has no {1} defined", this.assembly.FullName, typeof(T).Name);
        //    return customAttribute;
        //}

        [NotNull]
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private string DebuggerDisplay => $"specifics-of[{this.assembly.GetName().Name}.dll]";
    }
}
