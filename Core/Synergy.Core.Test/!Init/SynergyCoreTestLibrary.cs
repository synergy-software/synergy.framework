using System.Diagnostics.CodeAnalysis;
using Castle.Core.Logging;
using JetBrains.Annotations;
using Synergy.Core.Sample;

namespace Synergy.Core.Test
{
    public class SynergyCoreTestLibrary : Library
    {
        public SynergyCoreTestLibrary() : base(
            new SynergyCoreSampleLibrary()
        )
        {
        }

        [UsedImplicitly]
        [ExcludeFromCodeCoverage]
        internal static void References()
        {
            // ReSharper disable UnusedVariable.Compiler

            ILogger logger;

            // ReSharper restore UnusedVariable.Compiler
        }
    }
}