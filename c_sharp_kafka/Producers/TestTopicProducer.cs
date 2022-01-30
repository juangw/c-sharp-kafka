using System;
using c_sharp_kafka.Schemas;

namespace c_sharp_kafka.Producers
{
    class TestTopicProducer : IProducer<TestMessage>
    {
        public TestTopicProducer(string schemaHost, string bootstrapServers, string topicName)
        {
            this.schemaHost = schemaHost;
            this.bootstrapServers = bootstrapServers;
            this.topicName = topicName;
        }

        public override TestMessage BuildMessage(string payload)
        {
            Console.WriteLine($"Running Producer for {topicName} with payload: {payload}");
            
            return new TestMessage
            {
                body = payload
            };
        }
    }
}
