using Synergy.Core;

namespace Synergy.NHibernate
{
    public class SynergyNHibernateLibrary : Library
    {
        public SynergyNHibernateLibrary() : base(
            new SynergyCoreLibrary())
        {
        }
    }
}