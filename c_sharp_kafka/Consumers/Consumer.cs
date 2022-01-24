namespace c_sharp_kafka.Consumers
{
    interface Consumer
    {
        public abstract void Run(string schemaRegistryUrl, string bootstrapServers);
    }
}
