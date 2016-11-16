using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Synergy.Core;

namespace Synergy.NHibernate
{
    public class SynergyNHibernateLibrary : Library
    {
        public SynergyNHibernateLibrary() : base(new SynergyCoreLibrary())
        {
        }

        [UsedImplicitly]
        [ExcludeFromCodeCoverage]
        internal static void References()
        {
            // ReSharper disable UnusedVariable.Compiler
#pragma warning disable 414, CS0168

#pragma warning restore 414, CS0168
            // ReSharper restore UnusedVariable.Compiler
        }
    }
}