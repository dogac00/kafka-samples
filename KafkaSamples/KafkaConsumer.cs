using System.Threading;
using Confluent.Kafka;

#nullable enable

namespace KafkaSamples
{
    public class KafkaConsumer<TKey, TValue>
    {
        private readonly ConsumerConfig _config;
        
        /// <summary>
        /// Initialize a new KafkaConsumer object.
        /// </summary>
        /// <param name="groupId">GroupId should be given, otherwise it will throw an exception.</param>
        /// <param name="server">Server should be given to specify which server should we connect to.</param>
        public KafkaConsumer(string groupId, string server)
        {
            _config = new ConsumerConfig
            {
                BootstrapServers = server,
                GroupId = groupId
            };
        }
        
        /// <summary>
        /// Blocks until the next message is received from the consumer poll.
        /// </summary>
        /// <param name="token">For cancellation, optional.</param>
        /// <returns>The message object returned by the Consumer.</returns>
        public Message<TKey, TValue> NextMessage(TopicPartitionOffset tpa, CancellationToken token = default)
        {
            var builder = new ConsumerBuilder<TKey, TValue>(_config);

            using var consumer = builder.Build();
            
            consumer.Assign(tpa);

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
