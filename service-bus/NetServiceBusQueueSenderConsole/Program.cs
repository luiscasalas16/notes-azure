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

            Global.Helpers.ConsoleWait();
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
                // the client that owns the connection and can be used to create senders and receivers
                ServiceBusClient client;

                // the sender used to publish messages to the queue
                ServiceBusSender sender;

                // number of messages to be sent to the queue
                const int numOfMessages = 3;

                // the Service Bus client types are safe to cache and use as a singleton for the lifetime
                // of the application, which is best practice when messages are being published or read
                // regularly.

                // set the transport type to AmqpWebSockets so that the ServiceBusClient uses the port 443.
                var clientOptions = new ServiceBusClientOptions()
                {
                    //TransportType = ServiceBusTransportType.AmqpWebSockets, // comment for local emulator
                };
                client = new ServiceBusClient(
                    _configuration.GetValue<string>("ServiceBusKey"),
                    clientOptions
                );
                sender = client.CreateSender(_configuration.GetValue<string>("ServiceBusQueue"));

                // create a batch
                using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

                for (int i = 1; i <= numOfMessages; i++)
                {
                    // try adding a message to the batch
                    if (!messageBatch.TryAddMessage(new ServiceBusMessage($"Message {i}")))
                    {
                        // if it is too large for the batch
                        throw new Exception($"The message {i} is too large to fit in the batch.");
                    }
                }

                try
                {
                    // Use the producer client to send the batch of messages to the Service Bus queue
                    await sender.SendMessagesAsync(messageBatch);
                    Console.WriteLine(
                        $"A batch of {numOfMessages} messages has been published to the queue."
                    );
                }
                finally
                {
                    // Calling DisposeAsync on client types is required to ensure that network
                    // resources and other unmanaged objects are properly cleaned up.
                    await sender.DisposeAsync();
                    await client.DisposeAsync();
                }

                Global.Helpers.LogSuccess("Success");
            }
        }
    }
}
