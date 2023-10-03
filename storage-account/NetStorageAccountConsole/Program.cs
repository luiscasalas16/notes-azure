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

                const string SampleFileContent = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit.";

                string connectionString = "DefaultEndpointsProtocol=https;AccountName=lcs16sa;AccountKey=m4PFKJ4+mDeGEUu41DPVQua93l5ztpEzXEHcu5L9CQBhqSQGBZZF1nTc5vW1UkGOW2PNl4tkw7qx+ASt+5FKxg==;EndpointSuffix=core.windows.net";
                string containerName = $"{"sample-container"}-{DateTime.UtcNow.AddHours(-6).ToString("yyyy-MM-dd-HH-mm-ss-fffff")}";
                string blobName = $"{"sample-file"}-{DateTime.UtcNow.AddHours(-6).ToString("yyyy-MM-dd-HH-mm-ss-fffff")}";
                string filePath1 = Path.ChangeExtension(Path.GetTempFileName(), ".txt");
                string filePath2 = Path.ChangeExtension(Path.GetTempFileName(), ".txt");

                File.WriteAllText(filePath1, SampleFileContent);

                BlobContainerClient container = new BlobContainerClient(connectionString, containerName);

                container.Create();

                BlobClient blob = container.GetBlobClient(blobName);

                blob.Upload(filePath1);

                blob.DownloadTo(filePath2);

                if (File.ReadAllText(filePath1) != File.ReadAllText(filePath2))
                    throw new Exception("error");

                BlobProperties properties = blob.GetProperties();

                Console.WriteLine($"Container '{containerName}' Blob '{blobName}' MD5 '{Convert.ToBase64String(properties.ContentHash)}'");
            }
        }
    }
}