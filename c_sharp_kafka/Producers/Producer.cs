using Confluent.Kafka;
using System.Threading.Tasks;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using System;

namespace c_sharp_kafka.Producers
{
    public abstract class IProducer<T>
    {
        public string schemaHost;
        public string bootstrapServers;
        public string topicName;

        public abstract T BuildMessage(string payload);

        public virtual async Task Produce(string payload)
        {
            using var schemaRegistry = new CachedSchemaRegistryClient(new SchemaRegistryConfig { Url = schemaHost });
            using var producer =
                new ProducerBuilder<Null, T>(new ProducerConfig { BootstrapServers = bootstrapServers })
                    .SetValueSerializer(new AvroSerializer<T>(schemaRegistry))
                    .Build();
            await producer.ProduceAsync(topicName,
                new Message<Null, T>
                {
                    Value = BuildMessage(payload)
                });

            producer.Flush(TimeSpan.FromSeconds(30));
        }
    }
}
