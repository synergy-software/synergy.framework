using System.Diagnostics.CodeAnalysis;
using Castle.Core.Logging;
using JetBrains.Annotations;

namespace Synergy.Core
{
    /// <summary>
    ///     Library representing this assembly. You can reference it as a dependency but you don't have to - it will be added
    ///     to application libraries anyway.
    /// </summary>
    public class SynergyCoreLibrary : Library
    {
        /// <inheritdoc />
        public override bool SearchWindsorInstallersInThisAssembly => false;

        [UsedImplicitly]
        [ExcludeFromCodeCoverage]
        internal static void References()
        {
            // ReSharper disable UnusedVariable.Compiler
#pragma warning disable 414, CS0168

            ILogger logger;

#pragma warning restore 414, CS0168
            // ReSharper restore UnusedVariable.Compiler
        }
    }
}