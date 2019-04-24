using System.Threading.Tasks;

namespace Subscriber
{
    public interface IObservationClient
    {
        Task NewObservation(string observation);
    }
}
