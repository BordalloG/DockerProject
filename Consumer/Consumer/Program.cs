using Confluent.Kafka;
using System;
using System.Threading;

namespace Consumer
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
                Console.WriteLine("Ready to Consume events:");
                while (true)
                {
                    var cr = c.Consume(cts.Token);
                    Console.WriteLine($"Consumed message from topic {cr.Topic}, partition {cr.Partition}, offset {cr.Offset}. Message:");
                    Console.WriteLine(cr.Message.Value);
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
