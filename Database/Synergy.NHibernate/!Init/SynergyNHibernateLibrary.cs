using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Synergy.Core;

namespace Synergy.NHibernate
{
    /// <summary>
    /// Library with database support using NHibernate ORM.
    /// </summary>
    public class SynergyNHibernateLibrary : Library
    {
        /// <summary>
        /// Library with database support using NHibernate ORM.
        /// </summary>
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