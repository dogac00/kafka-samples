using System;
using Confluent.Kafka;

namespace KafkaSamples
{
    class Program
    {
        static void Main(string[] args) 
        {
            // These should be taken from a configuration file
            // This will be edited
            const string bootstrapServers = "YOUR_SERVER_NAME";
            const string groupId = "YOUR_GROUP_ID";
            
            var consumer = new KafkaConsumer<string, string>(groupId, bootstrapServers);

            var message = consumer.NextMessage(new TopicPartitionOffset("YOUR_TOPIC", 0, 0));

            Console.WriteLine(message);
        }
    }
    
    public class Sample {
        public int SumOfFirstThreeElements(int [] arr) {
            int sum = 0;
            for (int i = 0; i < 3; i++)
                sum += arr[i];
            return sum;
        }
    }
}
