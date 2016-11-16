using NHibernate.Cfg;

namespace Synergy.NHibernate.Configurations
{
    public class NHibernateConfigurationParameter
    {
        public const string ConnectionString = Environment.ConnectionString;

        //public void t()
        //{
        //    _defaultCatalog = PropertiesHelper.GetString(Environment.DefaultCatalog, configuration.Properties, null);
        //}
    }
}
