using Confluent.Kafka;
using System;
using System.Threading;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "PLAINTEXT://kafka:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var c = new ConsumerBuilder<Ignore, string>(conf).Build();
            c.Subscribe("quickstart-events");

            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) => {
                e.Cancel = true;
                cts.Cancel();
            };

            try
            {
                while (true)
                {
                    var cr = c.Consume(cts.Token);
                    Console.WriteLine($"Consumed message:\n'{cr.Message.Value}'\nfrom topic {cr.Topic}, partition {cr.Partition}, offset {cr.Offset}");
                }
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                c.Close();
            }
        }
    }
}
