using c_sharp_kafka.Schemas;
using System;

namespace c_sharp_kafka.Consumers
{
    class TestTopicConsumer : Consumer<TestMessage>
    {
        public TestTopicConsumer(string schemaHost, string bootstrapServers, string topicName, string groupId)
        {
            this.schemaHost = schemaHost;
            this.bootstrapServers = bootstrapServers;
            this.topicName = topicName;
            this.groupId = groupId;
        }

        public override void Run(TestMessage payload)
        {
            Console.WriteLine($"Running Consumer for {topicName}-{groupId} with payload: {payload.body}");
        }
    }
}
