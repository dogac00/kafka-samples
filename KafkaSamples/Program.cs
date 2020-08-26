using KafkaSamples.Consumer;
using System;

namespace KafkaSamples
{
    class Program
    {
        static void Main(string[] args) 
        {
            var consumer = new KafkaConsumer<string, string>();

            consumer.Subscribe("my-topic-to-subscribe");

            var result = consumer.Consume();

            if (result != null)
                consumer.CommitResult(result);

            Console.WriteLine(result?.Message);
        }
    }
}
