using c_sharp_kafka.Schemas;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using System;
using System.Threading;

namespace c_sharp_kafka.Consumers
{
    class TestTopic : Consumer
    {
        public static string topicName = "test-topic";
        public void Run(string schemaRegistryUrl, string bootstrapServers)
        {
            Console.WriteLine($"Running Consumer for {topicName}");

            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = bootstrapServers,
                // Note: The AutoOffsetReset property determines the start offset in the event
                // there are not yet any committed offsets for the consumer group for the
                // topic/partitions of interest. By default, offsets are committed
                // automatically, so in this example, consumption will only start from the
                // earliest message in the topic 'my-topic' the first time you run the program.
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var schemaRegistry = new CachedSchemaRegistryClient(new SchemaRegistryConfig { Url = schemaRegistryUrl }))
            using (var consumer =
                new ConsumerBuilder<Null, TestMessage>(conf)
                    .SetValueDeserializer(new AvroDeserializer<TestMessage>(schemaRegistry).AsSyncOverAsync())
                    .Build())
            {
                consumer.Subscribe(topicName);

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
                            var cr = consumer.Consume(cts.Token);
                            Console.WriteLine($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");
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
                    consumer.Close();
                }
            }
        }
    }
}
