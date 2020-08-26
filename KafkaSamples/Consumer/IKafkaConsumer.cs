using System;
using System.Threading;
using Confluent.Kafka;

namespace KafkaSamples.Consumer
{
    public interface IKafkaConsumer<TKey, TValue>
    {
        ConsumeResult<TKey, TValue> Consume();
        ConsumeResult<TKey, TValue> Consume(CancellationToken token);
        ConsumeResult<TKey, TValue> Consume(TimeSpan timeout);
        void CommitAll();
        void CommitResult(ConsumeResult<TKey, TValue> result);
    }
}