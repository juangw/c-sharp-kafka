using System;
using c_sharp_kafka.Utils;
using c_sharp_kafka.Schemas;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace c_sharp_kafka.Producers
{
    class WeatherTopicProducer : IProducer<WeatherMessage>
    {
        private static readonly string weatherApiUrl = "https://api.openweathermap.org/data/2.5/weather";
        public static readonly string weatherApiKey = Environment.GetEnvironmentVariable("weatherApiKey") ?? "";

        public WeatherTopicProducer(string schemaHost, string bootstrapServers, string topicName)
        {
            this.schemaHost = schemaHost;
            this.bootstrapServers = bootstrapServers;
            this.topicName = topicName;
        }

        public override WeatherMessage BuildMessage(string payload)
        {
            Console.WriteLine($"Running Producer for {topicName} with payload: {payload}");
            WeatherMessage weatherData = GetWeatherForCity(payload).Result;
            return weatherData;
        }

        public async Task<WeatherMessage> GetWeatherForCity(string city)
        {
            WeatherMessage weatherData = await Client.GetAsync<WeatherMessage>($"{weatherApiUrl}?q={city}&units=imperial&appid={weatherApiKey}");
            return weatherData;
        }
    }
}
