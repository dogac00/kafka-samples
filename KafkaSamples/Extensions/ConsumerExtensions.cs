using System.Linq;
using System.Reflection;
using Confluent.Kafka;

namespace KafkaSamples.Extensions
{
    public static class ConsumerExtensions
    {
        public static IDeserializer<TKey> GetKeyDeserializer<TKey, TValue>(this IConsumer<TKey, TValue> consumer)
        {
            var field = consumer.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(f => f.Name == "keyDeserializer");

            var keyDeserializer = field.GetValue(consumer);

            return keyDeserializer as IDeserializer<TKey>;
        }
        
        public static IDeserializer<TValue> GetValueDeserializer<TKey, TValue>(this IConsumer<TKey, TValue> consumer)
        {
            var field = consumer.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(f => f.Name == "valueDeserializer");

            var valueDeserializer = field.GetValue(consumer);

            return valueDeserializer as IDeserializer<TValue>;
        }
        
        public static ISerializer<TKey> GetKeySerializer<TKey, TValue>(this IConsumer<TKey, TValue> consumer)
        {
            var field = consumer.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(f => f.Name == "keySerializer");

            var keySerializer = field.GetValue(consumer);

            return keySerializer as ISerializer<TKey>;
        }
        
        public static ISerializer<TValue> GetValueSerializer<TKey, TValue>(this IConsumer<TKey, TValue> consumer)
        {
            var field = consumer.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(f => f.Name == "valueSerializer");

            var valueSerializer = field.GetValue(consumer);

            return valueSerializer as ISerializer<TValue>;
        }
    }
}