using Confluent.Kafka;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Producer
{
    class Program
    {
        public static async Task Main()
        {
            Thread.Sleep(5000);
            var config = new ProducerConfig
            {
                BootstrapServers = "kafka:9092"
            };

            using var p = new ProducerBuilder<Null, string>(config).Build();
            var counter = 0;
            while (true)
            {
                var text = "Mensagem: " + counter;
                counter++;
                var message = new Message<Null, string>
                {
                    Value = text
                };

                var dr = await p.ProduceAsync("quickstart-events", message);
                Console.WriteLine($"Produced message '{dr.Value}' to topic {dr.Topic}, partition {dr.Partition}, offset {dr.Offset}");

                Thread.Sleep(5000);
            }
        }
    }
}
