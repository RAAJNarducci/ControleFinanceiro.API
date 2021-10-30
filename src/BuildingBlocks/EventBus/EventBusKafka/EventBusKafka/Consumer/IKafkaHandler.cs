using System.Threading.Tasks;

namespace BuildingBlocks.EventBusKafka.Consumer
{
    public interface IKafkaHandler<Tk, Tv>
    {
        Task HandleAsync(Tk key, Tv value);
    }
}