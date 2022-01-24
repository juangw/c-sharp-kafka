using System;
using System.Collections.Generic;
using System.Text;

namespace c_sharp_kafka.Consumers
{
    class TestTopic : Consumer
    {
        public static string topicName = "test-topic";
        public void Run(string payload)
        {
            Console.WriteLine($"Running Consumer for {topicName} with payload: {payload}");
        }
    }
}
