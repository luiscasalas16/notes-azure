using System.Configuration;
using System.Web.Mvc;

namespace NetFwKeyVaultWebMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}