using Confluent.Kafka;

namespace BuildingBlocks.EventBusKafka.Producer
{
    public class KafkaProducerConfig<Tk, Tv> : ProducerConfig
    {
        public string Topic { get; set; }
    }
}