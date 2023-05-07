using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace keyvault_console
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
                    .ConfigureAppConfiguration((configuration) =>
                    {
                        var settings = configuration.Build();

                        Console.WriteLine(settings["SecretNameAppSettings"]);

                        TokenCredential credential = new DefaultAzureCredential();

                        configuration.AddAzureKeyVault(new Uri("https://luiscasalas16-key-vault.vault.azure.net/"), credential);
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

        class Application : IHostedService
        {
            private readonly ILogger<Application> _logger;
            private readonly IConfiguration _configuration;

            public Application(ILogger<Application> logger, IConfiguration configuration)
            {
                _logger = logger;
                _configuration = configuration;
            }

            private void Start()
            {
                Console.WriteLine($"SecretNameKeyVault {_configuration["SecretNameKeyVault"]}");
                Console.WriteLine($"SecretNameUserSecrets {_configuration["SecretNameUserSecrets"]}");
                Console.WriteLine($"SecretNameAppSettings {_configuration["SecretNameAppSettings"]}");
            }

            private void Stop()
            {
            }

            public Task StartAsync(CancellationToken cancellationToken)
            {
                Start();
                return Task.CompletedTask;
            }

            public Task StopAsync(CancellationToken cancellationToken)
            {
                Stop();
                return Task.CompletedTask;
            }
        }
    }
}