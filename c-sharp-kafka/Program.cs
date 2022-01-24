using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace c_sharp_kafka
{
    class Program
    {
        public static string host = Environment.GetEnvironmentVariable("host") ?? "localhost:9092";

        enum Modes
        {
            Producer,
            Consumer
        }

        public static Task Main(string[] args)
        {
            if (args.Length < 2)
            {
                throw new ArgumentException("There should be at least 2 arguments for this application");
            }

            string mode = args[0];
            if (!Enum.TryParse(mode, out Modes _))
            {
                throw new ArgumentException("Invalid mode argument value provided. Must be either: `Consumer` or `Producer`");
            }
            
            string topic = args[1];
            switch (mode.ToLower())
            {
                case "consumer":
                    Console.WriteLine("Creating Consumers & Consuming Message");
                    ConsumeMessage(topic);
                    return Task.CompletedTask;
                case "producer":
                    Console.WriteLine("Creating Producer & Producing Message");
                    string message = args[2];
                    return ProduceMessage(topic, message);
                default:
                    throw new ArgumentException($"Run Mode Provided: {mode}, Must be either `consumer` or `producer`");
            }

        }

        private static async Task ProduceMessage(string topic, string payload)
        {
            var config = new ProducerConfig { BootstrapServers = host };

            // If serializers are not specified, default serializers from
            // `Confluent.Kafka.Serializers` will be automatically used where
            // available. Note: by default strings are encoded as UTF8.
            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var dr = await p.ProduceAsync(topic, new Message<Null, string> { Value = payload });
                    Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
        }

        private static void ConsumeMessage(string topic)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = host,
                // Note: The AutoOffsetReset property determines the start offset in the event
                // there are not yet any committed offsets for the consumer group for the
                // topic/partitions of interest. By default, offsets are committed
                // automatically, so in this example, consumption will only start from the
                // earliest message in the topic 'my-topic' the first time you run the program.
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var c = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                c.Subscribe(topic);

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) => {
                    e.Cancel = true; // prevent the process from terminating.
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var cr = c.Consume(cts.Token);
                            Console.WriteLine($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");
                            switch (topic)
                            {
                                case "test-topic":
                                    new Consumers.TestTopic().Run(cr.Message.Value);
                                    break;
                                default:
                                    Console.WriteLine($"Topic: {topic} doesn't have a configured consumer action");
                                    break;
                            }
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    c.Close();
                }
            }
        }
    }
}
