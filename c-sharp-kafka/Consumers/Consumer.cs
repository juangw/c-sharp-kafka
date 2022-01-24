using System;
using System.Collections.Generic;
using System.Text;

namespace c_sharp_kafka.Consumers
{
    interface Consumer
    {
        public static string topicName;
        public abstract void Run(string payload);
    }
}
