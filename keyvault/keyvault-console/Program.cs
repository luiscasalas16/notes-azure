using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
                        /*
                        "AZURE_TENANT_ID": "c3db3a9b-847b-4bbe-b592-62ad5d4f6918",
                        "AZURE_CLIENT_ID": "c13ff45e-b0aa-495e-865d-87714cea7d39",
                        "AZURE_CLIENT_SECRET": "3ef8Q~2ILBT-Hn~6h-L3c01XwM85cDE~6c9-Pbyc"
                        */

                        /*
                        //service principal in multiple locations

                        TokenCredential credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions()
                        {
                            ExcludeVisualStudioCredential = true,
                            ExcludeVisualStudioCodeCredential = true,
                            ExcludeAzureCliCredential = true,
                            ExcludeAzurePowerShellCredential = true
                        });
                        */

                        //service principal in appsettings.json

                        var settings = configuration.Build();

                        TokenCredential credential = new ClientSecretCredential
                        (
                            settings["AZURE_TENANT_ID"],
                            settings["AZURE_CLIENT_ID"],
                            settings["AZURE_CLIENT_SECRET"]
                        );

                        configuration.AddAzureKeyVault(new Uri("https://luiscasalas16-key-vault.vault.azure.net/"), credential);
                    })
                    .Build();

                host.Run();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
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