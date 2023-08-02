using System.Configuration;
using System.Web.Mvc;

namespace keyvault_netfw_web_mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.SecretNameKeyVault = ConfigurationManager.AppSettings["SecretNameKeyVault"];
            ViewBag.SecretNameUserSecrets = ConfigurationManager.AppSettings["SecretNameUserSecrets"];
            ViewBag.SecretNameAppSettings = ConfigurationManager.AppSettings["SecretNameAppSettings"];

            return View();
        }
    }
}