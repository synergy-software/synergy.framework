using Synergy.Core;
using Synergy.NHibernate.Configurations;
using Synergy.NHibernate.Sample;
using Synergy.WindsorCastle.Libraries;

namespace Synergy.NHibernate.Test
{
    public class SynergyNHibernateTestLibrary : Library
    {
        public SynergyNHibernateTestLibrary() : base(
            new SynergyNHibernateSampleLibrary(),
            new ExternalLibrary(typeof(NHibernateConfigurationParameter))
            )
        {
        }
    }
}