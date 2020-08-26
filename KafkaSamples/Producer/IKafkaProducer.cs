using Confluent.Kafka;
using System.Threading.Tasks;

namespace KafkaSamples.Producer
{
    public interface IKafkaProducer<TKey, TValue>
    {
        Task ProduceAsync(string topic, TKey key, TValue value);

        Task ProduceAsync(TopicPartition tp, TKey key, TValue value);
    }
}
