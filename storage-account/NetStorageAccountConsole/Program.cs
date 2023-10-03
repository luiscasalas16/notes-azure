using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace NetStorageAccountConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using IHost host = Host.CreateDefaultBuilder(args)
                    .ConfigureServices((services) =>
                    {
                        services.AddHostedService<Application>();
                    })
                    .Build();

                host.Run();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());

                Console.WriteLine(ex.ToString());
            }
        }

        public class Application : BackgroundService
        {
            private readonly ILogger<Application> _logger;
            private readonly IConfiguration _configuration;

            public Application(ILogger<Application> logger, IConfiguration configuration)
            {
                _logger = logger;
                _configuration = configuration;
            }

            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
                Console.WriteLine(".Net Console");

                string result;

                try
                {
                    const string SampleFileContent = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit.";

                    string containerName = $"{"sample-container"}-{DateTime.UtcNow.AddHours(-6).ToString("yyyy-MM-dd-HH-mm-ss-fffff")}";
                    string blobName = $"{"sample-file"}-{DateTime.UtcNow.AddHours(-6).ToString("yyyy-MM-dd-HH-mm-ss-fffff")}";
                    string filePath1 = Path.ChangeExtension(Path.GetTempFileName(), ".txt");
                    string filePath2 = Path.ChangeExtension(Path.GetTempFileName(), ".txt");

                    System.IO.File.WriteAllText(filePath1, SampleFileContent);

                    BlobServiceClient client = new BlobServiceClient(new Uri($"https://lcs16sa.blob.core.windows.net"), new DefaultAzureCredential());

                    BlobContainerClient container = client.CreateBlobContainer(containerName);

                    container.CreateIfNotExists();

                    BlobClient blob = container.GetBlobClient(blobName);

                    blob.Upload(filePath1);

                    blob.DownloadTo(filePath2);

                    if (System.IO.File.ReadAllText(filePath1) != System.IO.File.ReadAllText(filePath2))
                        throw new Exception("error");

                    BlobProperties properties = blob.GetProperties();

                    result = $"Container '{containerName}' Blob '{blobName}' MD5 '{Convert.ToBase64String(properties.ContentHash)}'";
                }
                catch (Exception ex)
                {
                    result = ex.ToString();
                }

                Console.WriteLine(result);
            }
        }
    }
}