using System.Web.Mvc;
using JetBrains.Annotations;

namespace Synergy.NHibernate.Sample
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters([NotNull] GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
