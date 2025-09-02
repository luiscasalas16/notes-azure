using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NetBaseConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using IHost host = Host.CreateDefaultBuilder(args)
                    .ConfigureServices(
                        (services) =>
                        {
                            services.AddHostedService<Application>();
                        }
                    )
                    .Build();

                host.Run();
            }
            catch (Exception ex)
            {
                Global.Helpers.LogFailure(ex);
            }
        }

        public class Application : BackgroundService
        {
            private readonly ILogger<Application> _logger;
            private readonly IConfiguration _configuration;

            // the client that owns the connection and can be used to create senders and receivers
            ServiceBusClient client;

            // the processor that reads and processes messages from the queue
            ServiceBusProcessor processor;

            public Application(ILogger<Application> logger, IConfiguration configuration)
            {
                _logger = logger;
                _configuration = configuration;
            }

            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
                // the Service Bus client types are safe to cache and use as a singleton for the lifetime
                // of the application, which is best practice when messages are being published or read
                // regularly.

                // set the transport type to AmqpWebSockets so that the ServiceBusClient uses port 443.
                var clientOptions = new ServiceBusClientOptions()
                {
                    //TransportType = ServiceBusTransportType.AmqpWebSockets, // comment for local emulator
                };
                client = new ServiceBusClient(
                    _configuration.GetValue<string>("ServiceBusKey"),
                    clientOptions
                );

                // create a processor that we can use to process the messages
                processor = client.CreateProcessor(
                    _configuration.GetValue<string>("ServiceBusQueue"),
                    new ServiceBusProcessorOptions()
                );

                try
                {
                    // add handler to process messages
                    processor.ProcessMessageAsync += MessageHandler;

                    // add handler to process any errors
                    processor.ProcessErrorAsync += ErrorHandler;

                    // start processing
                    await processor.StartProcessingAsync();

                    Console.WriteLine(
                        "Wait for a minute and then press any key to end the processing"
                    );
                    Console.ReadKey();

                    // stop processing
                    Console.WriteLine("Stopping receiver messages");
                    await processor.StopProcessingAsync();
                    Console.WriteLine("Stopped receiver messages");
                }
                finally
                {
                    // Calling DisposeAsync on client types is required to ensure that network
                    // resources and other unmanaged objects are properly cleaned up.
                    await processor.DisposeAsync();
                    await client.DisposeAsync();
                }

                Global.Helpers.LogSuccess("Success");
                Global.Helpers.ConsoleWait();
            }

            // handle received messages
            async Task MessageHandler(ProcessMessageEventArgs args)
            {
                string body = args.Message.Body.ToString();

                Console.WriteLine($"Received: {body}");

                // complete the message. message is deleted from the queue.
                await args.CompleteMessageAsync(args.Message);
            }

            // handle any errors when receiving messages
            Task ErrorHandler(ProcessErrorEventArgs args)
            {
                Console.WriteLine(args.Exception.ToString());

                return Task.CompletedTask;
            }
        }
    }
}
