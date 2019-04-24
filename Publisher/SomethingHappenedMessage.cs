using System;
using Messages;

namespace Publisher
{
    internal class SomethingHappenedMessage : ISomethingHappened
    {
        public DateTime Time { get; set; }
        public string Observation { get; set; }
    }
}
