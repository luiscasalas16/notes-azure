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

        [HttpPost]
        public ActionResult Tests()
        {
            return Json(new
            {
                result = "ok"
            });
        }
    }
}