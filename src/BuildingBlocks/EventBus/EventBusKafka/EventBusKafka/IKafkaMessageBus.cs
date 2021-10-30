using System.Threading.Tasks;

namespace BuildingBlocks.EventBusKafka
{
    public interface IKafkaMessageBus<Tk, Tv>
    {
        Task PublishAsync(Tk key, Tv message);
    }
}