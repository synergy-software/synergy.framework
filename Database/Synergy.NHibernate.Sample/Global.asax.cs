using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Synergy.Core.Windsor;
using Synergy.NHibernate.Sample.Domain;

namespace Synergy.NHibernate.Sample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var rootLibrary = new SynergyNHibernateSampleLibrary();
            IWindsorEngine windsorEngine = new WindsorEngine();
            windsorEngine.Start(rootLibrary);
            var db = windsorEngine.GetComponent<ISampleDatabase>();
            db.Open();

            var controllerFactory = windsorEngine.GetComponent<IControllerFactory>();
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
 
        }
    }
}
