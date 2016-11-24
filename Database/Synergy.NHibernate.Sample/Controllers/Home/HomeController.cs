using System.Web.Mvc;

namespace Synergy.NHibernate.Sample.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult About()
        {
            this.ViewBag.Message = "Your application description page.";

            return this.View();
        }

        public ActionResult Contact()
        {
            this.ViewBag.Message = "Your contact page.";

            return this.View();
        }

        public ActionResult CreateDatabase()
        {
            this.homeService.CreateDatabaseSchema();

            return this.View("Index");
        }

        public ActionResult InvokeAction()
        {
            this.homeService.InvokeAnotherSession();

            return this.View("Index");
        }
    }
}