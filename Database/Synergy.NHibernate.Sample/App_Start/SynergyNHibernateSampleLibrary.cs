using Synergy.Core;
using Synergy.Web.Mvc;

namespace Synergy.NHibernate.Sample
{
    public class SynergyNHibernateSampleLibrary : Library
    {
        public SynergyNHibernateSampleLibrary() : base(
            new SynergyWebMvcLibrary(),
            new SynergyNHibernateLibrary(),
            new SynergyCoreLibrary())
        {
        }
    }
}