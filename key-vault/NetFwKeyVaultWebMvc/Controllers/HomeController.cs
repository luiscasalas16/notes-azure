using System.Configuration;
using System.Web.Mvc;

namespace NetFwKeyVaultWebMvc.Controllers
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