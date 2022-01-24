using System.Threading.Tasks;

namespace c_sharp_kafka.Producers
{
    interface Producer
    {
        public abstract Task Send(string schemaRegistryUrl, string bootstrapServers, string topic, string payload);
    }
}
