using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;

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
                    await new Producers.TestTopic().Send(schemaHost, host, topic, payload);
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
                    new Consumers.TestTopic().Run(schemaHost, host);
                    break;
                default:
                    Console.WriteLine($"Topic: {topic} doesn't have a configured consumer action");
                    break;
            }
        }
    }
}
