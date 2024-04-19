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
            Log("Test");

            try
            {
                HttpClient client = new HttpClient();

                string url = _configuration.GetValue<string>("Api") ?? string.Empty;

                Log($"Api : '{url}'");

                var response = await client.GetAsync($"{url}/api/weatherforecast");

                if (!response.IsSuccessStatusCode)
                {
                    Log($"result = {response.StatusCode}");
                    return Json(new { result = response.StatusCode });
                }

                var content = await response.Content.ReadAsStringAsync();

                Log($"result = {content}");
                return Json(new { result = content });
            }
            catch (Exception ex)
            {
                Log($"result = {ex.ToString()}");
                return Json(new { result = "Exception" });
            }
        }

        private void Log(string message)
        {
            _logger.LogInformation(message);
            Console.WriteLine(message);
        }
    }
}
