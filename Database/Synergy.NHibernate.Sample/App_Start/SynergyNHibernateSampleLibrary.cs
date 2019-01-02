using Synergy.Core;
using Synergy.Core.Web._Init;
using Synergy.Web.Mvc;

namespace Synergy.NHibernate.Sample
{
    public class SynergyNHibernateSampleLibrary : Library
    {
        public SynergyNHibernateSampleLibrary() : base(
            new SynergyCoreWebLibrary(),
            new SynergyWebMvcLibrary(),
            new SynergyNHibernateLibrary())
        {
        }
    }
}