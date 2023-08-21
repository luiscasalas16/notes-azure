using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace NetWebConfigurationConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("url:");

                string url = Console.ReadLine()!;

                using IHost host = Host.CreateDefaultBuilder(args)
                    .ConfigureServices((services) =>
                    {
                        services.AddHostedService(serviceProvider =>new Application(url, serviceProvider.GetService<ILogger<Application>>()!));
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
            private readonly string _url;
            private readonly ILogger<Application> _logger;

            public Application(string url, ILogger<Application> logger)
            {
                _url = url;
                _logger = logger;
            }

            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
                int i = 1;

                while (!stoppingToken.IsCancellationRequested)
                {
                    var response = await new HttpClient().SendAsync(new HttpRequestMessage(HttpMethod.Get, _url));

                    response.EnsureSuccessStatusCode();

                    string content = await response.Content.ReadAsStringAsync();

                    var match = Regex.Match(content, @"WEBSITE_INSTANCE_ID : (\w+)");

                    Console.WriteLine($"{match.Groups[1].Value} - {i++}");
                }
            }
        }
    }
}