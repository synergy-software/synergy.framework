using Synergy.Core;

namespace Synergy.NHibernate.Test
{
    public class SynergyNHibernateTestLibrary : Library
    {
        public SynergyNHibernateTestLibrary() : base(
            new SynergyNHibernateLibrary()
            )
        {
        }
    }
}