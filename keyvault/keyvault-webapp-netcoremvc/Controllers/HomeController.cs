using Microsoft.AspNetCore.Mvc;

namespace keyvault_webapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.SecretNameKeyVault = _configuration["SecretNameKeyVault"];
            ViewBag.SecretNameUserSecrets = _configuration["SecretNameUserSecrets"];
            ViewBag.SecretNameAppSettings = _configuration["SecretNameAppSettings"];

            return View();
        }
    }
}