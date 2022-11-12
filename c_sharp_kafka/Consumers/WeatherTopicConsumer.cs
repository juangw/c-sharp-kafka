using c_sharp_kafka.Schemas;
using System;

namespace c_sharp_kafka.Consumers
{
    class WeatherTopicConsumer : Consumer<WeatherMessage>
    {
        public WeatherTopicConsumer(string schemaHost, string bootstrapServers, string topicName, string groupId)
        {
            this.schemaHost = schemaHost;
            this.bootstrapServers = bootstrapServers;
            this.topicName = topicName;
            this.groupId = groupId;
        }

        public override void Run(WeatherMessage payload)
        {
            Console.WriteLine($"Running Consumer for {topicName}-{groupId} for city: {payload.name}");
            Console.WriteLine($"Main: {payload.main.temp}");
        }
    }
}
