using JetBrains.Annotations;

namespace Synergy.Core.Sample
{
    public class SynergyCoreSampleLibrary : Library
    {
        public override bool PopulateComponentsAutomatically => false;

        [UsedImplicitly]
        internal static void References()
        {
            // ReSharper disable UnusedVariable.Compiler

            // ReSharper restore UnusedVariable.Compiler
        }
    }
}