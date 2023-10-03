using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;

namespace NetStorageAccountWebMvc.Controllers
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
            const string SampleFileContent = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit.";

            string containerName = $"{"sample-container"}-{DateTime.UtcNow.AddHours(-6).ToString("yyyy-MM-dd-HH-mm-ss-fffff")}";
            string blobName = $"{"sample-file"}-{DateTime.UtcNow.AddHours(-6).ToString("yyyy-MM-dd-HH-mm-ss-fffff")}";
            string filePath1 = Path.ChangeExtension(Path.GetTempFileName(), ".txt");
            string filePath2 = Path.ChangeExtension(Path.GetTempFileName(), ".txt");

            System.IO.File.WriteAllText(filePath1, SampleFileContent);

            BlobServiceClient client = new(new Uri($"https://lcs16sa.blob.core.windows.net"), new DefaultAzureCredential());

            BlobContainerClient container = client.CreateBlobContainer(containerName);

            container.Create();

            BlobClient blob = container.GetBlobClient(blobName);

            blob.Upload(filePath1);

            blob.DownloadTo(filePath2);

            if (System.IO.File.ReadAllText(filePath1) != System.IO.File.ReadAllText(filePath2))
                throw new Exception("error");

            BlobProperties properties = blob.GetProperties();

            ViewBag.result = $"Container '{containerName}' Blob '{blobName}' MD5 '{Convert.ToBase64String(properties.ContentHash)}'";

            return View();
        }
    }
}