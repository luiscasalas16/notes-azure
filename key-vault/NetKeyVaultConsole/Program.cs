using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;

namespace NetKeyVaultConsole
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
                        //Secrets
                        configuration.AddUserSecrets<Program>();

                        //KeyVault
                        configuration.AddAzureKeyVault(new Uri("https://lcs16-kv.vault.azure.net/"), new DefaultAzureCredential());
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
                Console.WriteLine($"SecretNameKeyVault: {_configuration["SecretNameKeyVault"]}");
                Console.WriteLine($"SecretNameUserSecrets: {_configuration["SecretNameUserSecrets"]}");
                Console.WriteLine($"SecretNameAppSettings: {_configuration["SecretNameAppSettings"]}");
            }
        }
    }
}