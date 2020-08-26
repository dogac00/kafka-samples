using System;
using System.Threading;
using Confluent.Kafka;

namespace KafkaSamples.Consumer
{
    public class KafkaConsumer<TKey, TValue> : IDisposable, IKafkaConsumer<TKey, TValue>
    {
        private readonly IConsumer<TKey, TValue> _consumer;

        public KafkaConsumer()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "default-consumer-server",
                SessionTimeoutMs = 5000
            };

            _consumer = new ConsumerBuilder<TKey, TValue>(config).Build();
        }

        public KafkaConsumer(ConsumerConfig config)
        {
            _consumer = new ConsumerBuilder<TKey, TValue>(config).Build();
        }

        public void Subscribe(string topic)
        {
            _consumer.Subscribe(topic);
        }

        public ConsumeResult<TKey, TValue> Consume()
        {
            return _consumer.Consume();
        }

        public ConsumeResult<TKey, TValue> Consume(CancellationToken token)
        {
            return _consumer.Consume(token);
        }

        public ConsumeResult<TKey, TValue> Consume(TimeSpan timeout)
        {
            return _consumer.Consume(timeout);
        }

        public void CommitAll()
        {
            _consumer.Commit();
        }

        public void CommitResult(ConsumeResult<TKey, TValue> result)
        {
            _consumer.Commit(result);
        }

        public void Dispose()
        {
            _consumer?.Dispose();
        }
    }
}