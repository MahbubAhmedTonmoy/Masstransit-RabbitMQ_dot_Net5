using Confluent.Kafka;
using System;

namespace consumerMS
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConsumerConfig
            {
                GroupId = "gid-consumers",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Null, string>(config).Build())
            {
                consumer.Subscribe("abc");
                while (true)
                {
                    var cr = consumer.Consume();
                    Console.WriteLine(cr.Message.Value);
                }
            }
        }
    }
}
