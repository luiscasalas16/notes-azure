using System.Configuration;
using System.Web.Mvc;

namespace NetFwApplicationServiceMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}