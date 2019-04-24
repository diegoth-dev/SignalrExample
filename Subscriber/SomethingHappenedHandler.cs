using System;
using System.Threading.Tasks;
using Messages;
using Microsoft.AspNetCore.SignalR;
using NServiceBus;
using NServiceBus.Logging;

namespace Subscriber
{
    internal class SomethingHappenedHandler : IHandleMessages<ISomethingHappened>
    {
        private static readonly ILog Log = LogManager.GetLogger<SomethingHappenedHandler>();

        public Task Handle(ISomethingHappened message, IMessageHandlerContext context)
        {
            Log.Info($"Received SomethingHappenedMessage. Time: {message.Time}, Observation: {message.Observation}.");

            try
            {
                Startup.ObservationHub.Clients.All.NewObservation(message.Observation);
                return Task.CompletedTask;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}
