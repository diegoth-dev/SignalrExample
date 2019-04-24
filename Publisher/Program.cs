using System;
using System.Threading.Tasks;
using NServiceBus;

namespace Publisher
{
    internal class Program
    {
        internal static async Task Main()
        {
            const string endpointName = "Samples.PubSub.Publisher";
            Console.Title = endpointName;
            var endpointConfiguration = new EndpointConfiguration(endpointName);
            
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.ConnectionString("host=localhost");
            transport.UseConventionalRoutingTopology();

            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
            await Start(endpointInstance)
                .ConfigureAwait(false);
            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }

        private static async Task Start(IMessageSession endpointInstance)
        {
            Console.WriteLine("Press '1' to publish the OrderReceived event");
            Console.WriteLine("Press any other key to exit");

            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();

                var observation = Guid.NewGuid().ToString();
                var time = DateTime.Now;

                if (key.Key == ConsoleKey.D1)
                {
                    var somethingHappenedMessage = new SomethingHappenedMessage
                    {
                        Time = time,
                        Observation = observation
                    };

                    await endpointInstance.Publish(somethingHappenedMessage)
                        .ConfigureAwait(false);
                    Console.WriteLine($"Published SomethingHappenedMessage. Time: {time}, Observation: {observation}.");
                }
                else
                {
                    return;
                }
            }
        }
    }
}
