using Synergy.Core;
using Synergy.NHibernate.Sample;

namespace Synergy.NHibernate.Test
{
    public class SynergyNHibernateTestLibrary : Library
    {
        public SynergyNHibernateTestLibrary() : base(
            new SynergyNHibernateSampleLibrary(),
            new SynergyNHibernateLibrary(),
            new SynergyCoreLibrary()
            )
        {
        }
    }
}