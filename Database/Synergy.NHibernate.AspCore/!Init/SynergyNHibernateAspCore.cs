using Synergy.Core;

namespace Synergy.NHibernate.AspCore._Init
{
    public class SynergyNHibernateAspCore:Library
    {
        /// <inheritdoc />
        public SynergyNHibernateAspCore() : base(new SynergyNHibernateLibrary())
        {
        }
    }
}
