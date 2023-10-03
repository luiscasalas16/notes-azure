using System;
using System.Web.Mvc;

namespace NetFwStorageAccountWebMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.t = DateTime.Now.ToString();

            return View();
        }
    }
}