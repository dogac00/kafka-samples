using Confluent.Kafka;
using System;
using System.Runtime.CompilerServices;

namespace KafkaSamples.Extensions
{
    public static class MessageExtensions
    {
        public static T GetHeader<T>(this Message<string, string> message, string key)
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
        
        public static void SetHeader<T>(this Message<string, string> message, string key, T value)
        {
            byte[] bytes = new byte[Unsafe.SizeOf<T>()];

            Unsafe.As<byte, T>(ref bytes[0]) = value;

            message.Headers.Add(key, bytes);
        }
    }
}
