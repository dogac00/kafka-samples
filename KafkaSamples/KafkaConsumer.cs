using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace KafkaSamples
{
    public class KafkaConsumer
    {
        public static async Task<Message<TKey, TValue>> ListenAsync<TKey, TValue>(
            string bootstrapServers, 
            CancellationToken token = default)
        {
            ConsumerConfig config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                EnableAutoCommit = true
            };

            var builder = new ConsumerBuilder<TKey, TValue>(config);

            using var consumer = builder.Build();

            while (true)
            {
                var consumeResult = consumer.Consume(token);

                if (consumeResult.IsPartitionEOF)
                    return default;

                return consumeResult.Message;
            }
        }
    }
}
