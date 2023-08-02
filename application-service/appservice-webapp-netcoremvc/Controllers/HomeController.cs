using Microsoft.AspNetCore.Mvc;

namespace keyvault_webapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult tests()
        {
            return Json(new
            {
                result = "ok"
            });
        }
    }
}