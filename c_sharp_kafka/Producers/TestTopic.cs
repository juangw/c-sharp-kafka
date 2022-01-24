using Confluent.Kafka;
using System;
using System.Threading.Tasks;
using c_sharp_kafka.Schemas;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

namespace c_sharp_kafka.Producers
{
    class TestTopic : Producer
    {
        public static string topicName = "test-topic";
        public async Task Send(string schemaRegistryUrl, string bootstrapServers, string topic, string payload)
        {
            Console.WriteLine($"Running Producer for {topicName} with payload: {payload}");

            using (var schemaRegistry = new CachedSchemaRegistryClient(new SchemaRegistryConfig { Url = schemaRegistryUrl }))
            using (var producer =
                new ProducerBuilder<Null, TestMessage>(new ProducerConfig { BootstrapServers = bootstrapServers })
                    .SetValueSerializer(new AvroSerializer<TestMessage>(schemaRegistry))
                    .Build())
            {
                await producer.ProduceAsync(topic,
                    new Message<Null, TestMessage>
                    {
                        Value = new TestMessage
                        {
                            body = payload
                        }
                    });

                producer.Flush(TimeSpan.FromSeconds(30));
            }
        }
    }
}
