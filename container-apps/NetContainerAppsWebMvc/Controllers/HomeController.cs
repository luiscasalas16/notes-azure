using Microsoft.AspNetCore.Mvc;

namespace NetContainerAppsWebMvc.Controllers
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Test()
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync(_configuration.GetValue<string>("Api"));

            if (!response.IsSuccessStatusCode)
                return Json(new { result = response.StatusCode });

            var content = await response.Content.ReadAsStringAsync();

            return Json(new { result = content });
        }
    }
}
