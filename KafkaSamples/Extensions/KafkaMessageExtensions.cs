using Confluent.Kafka;
using System;
using System.Runtime.CompilerServices;

namespace KafkaSamples.Extensions
{
    public static class MessageExtensions
    {
        public static T GetHeader<T>(this Message<string, string> message, string key) where T : unmanaged
        {
            foreach (var header in message.Headers)
            {
                if (header.Key == key)
                {
                    var bytes = header.GetValueBytes();

                    return Unsafe.As<byte, T>(ref bytes[0]);
                }
            }
            
            throw new Exception("Header not found.");
        }
        
        public static void SetHeader<T>(this Message<string, string> message, string key, T value) where T : unmanaged
        {
            byte[] bytes = new byte[Unsafe.SizeOf<T>()];

            Unsafe.As<byte, T>(ref bytes[0]) = value;

            message.Headers.Add(key, bytes);
        }
        
        public static Message<TKey, TValue> AsGeneric<TKey, TValue>(this Message<byte[], byte[]> message,
            IDeserializer<TKey> keyDeserializer,
            IDeserializer<TValue> valueDeserializer)
        {
            if (message == null)
            {
                return new Message<TKey, TValue>
                {
                    Key = default,
                    Value = default
                };
            }

            var key = keyDeserializer.Deserialize(message.Key.AsSpan(),
                false,
                new SerializationContext());
            var value = valueDeserializer.Deserialize(message.Value.AsSpan(),
                false,
                new SerializationContext());
            
            return new Message<TKey, TValue>
            {
                Key = key,
                Value = value,
                Headers = message.Headers,
                Timestamp = message.Timestamp
            };
        }
    }
}
