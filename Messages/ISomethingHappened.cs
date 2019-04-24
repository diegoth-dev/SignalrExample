using System;
using NServiceBus;

namespace Messages
{
    public interface ISomethingHappened : IEvent
    {
        DateTime Time { get; set; }
        string Observation { get; set; }
    }
}
