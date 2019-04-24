using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NServiceBus;

namespace Subscriber
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var endpointInstance = await ConfigureBusEndpoint();
            CreateWebHostBuilder().Build().Run();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            await endpointInstance.Stop().ConfigureAwait(false);
        }

        private static async Task<IEndpointInstance> ConfigureBusEndpoint()
        {
            const string endpointName = "Samples.PubSub.Subscriber";
            Console.Title = endpointName;
            var endpointConfiguration = new EndpointConfiguration(endpointName);

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.ConnectionString("host=localhost");
            transport.UseConventionalRoutingTopology();

            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            return await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
        }

        private static IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder().UseStartup<Startup>();
        }   
    }
}
