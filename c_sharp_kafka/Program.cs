using System;
using System.IO;
using System.Threading.Tasks;
using c_sharp_kafka.Consumers;
using c_sharp_kafka.Producers;

namespace c_sharp_kafka
{
    class Program
    {
        public static string host = Environment.GetEnvironmentVariable("host") ?? "localhost:9092";
        public static string schemaHost = Environment.GetEnvironmentVariable("schemaHost") ?? "localhost:8081";

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

            var root = Directory.GetCurrentDirectory();
            var dotenv = Path.Combine(root, ".env");
            DotEnv.Load(dotenv);
            
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
            switch (topic)
            {
                case "test-topic":
                    TestTopicProducer testTopicProducer = new TestTopicProducer(schemaHost, host, topic);
                    await testTopicProducer.Produce(payload);
                    break;
                case "weather-topic":
                    WeatherTopicProducer weatherTopicProducer = new WeatherTopicProducer(schemaHost, host, topic);
                    await weatherTopicProducer.Produce(payload);
                    break;
                default:
                    Console.WriteLine($"Topic: {topic} doesn't have a configured producer action");
                    break;
            }
        }

        private static void ConsumeMessage(string topic)
        {
            switch (topic)
            {
                case "test-topic":
                    TestTopicConsumer testTopicConsumer = new TestTopicConsumer(schemaHost, host, topic, "test-consumer-group");
                    testTopicConsumer.Consume();
                    break;
                case "weather-topic":
                    WeatherTopicConsumer weatherTopicConsumer = new WeatherTopicConsumer(schemaHost, host, topic, "weather-consumer-group");
                    weatherTopicConsumer.Consume();
                    break;
                default:
                    Console.WriteLine($"Topic: {topic} doesn't have a configured consumer action");
                    break;
            }
        }
    }
}
