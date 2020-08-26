using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace KafkaSamples.Producer
{
    public class KafkaProducer<TKey, TValue> : IKafkaProducer<TKey, TValue>, IDisposable
    {
        private readonly IProducer<TKey, TValue> _producer;

        public KafkaProducer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "my-servers",
                RequestTimeoutMs = 5000
            };

            _producer = new ProducerBuilder<TKey, TValue>(config).Build();
        }

        public KafkaProducer(ProducerConfig config)
        {
            _producer = new ProducerBuilder<TKey, TValue>(config).Build();
        }

        public void Dispose()
        {
            _producer?.Dispose();
        }

        public async Task ProduceAsync(string topic, TKey key, TValue value)
        {
            await ProduceAsync(new TopicPartition(topic, Partition.Any), key, value);
        }

        public async Task ProduceAsync(TopicPartition tp, TKey key, TValue value)
        {
            await _producer.ProduceAsync(tp, new Message<TKey, TValue>());
        }
    }
}