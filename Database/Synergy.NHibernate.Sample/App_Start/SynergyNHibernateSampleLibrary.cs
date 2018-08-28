using Synergy.Core;
using Synergy.Core.Web._Init;
using Synergy.NHibernate.Configurations;
using Synergy.Web.Mvc;
using Synergy.WindsorCastle.Libraries;

namespace Synergy.NHibernate.Sample
{
    public class SynergyNHibernateSampleLibrary : Library
    {
        public SynergyNHibernateSampleLibrary() : base(
            new SynergyCoreWebLibrary(),
            new SynergyWebMvcLibrary(),
            new ExternalLibrary(typeof(NHibernateConfigurationParameter)))
        {
        }
    }
}