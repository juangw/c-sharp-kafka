using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace c_sharp_kafka.Utils
{
    class Client
    {
        private static readonly HttpClient client = new HttpClient();
        
        public static async Task<T> GetAsync<T>(string url)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<T>();
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine($"Exception during GET request: {e.Message}");
                throw e;
            }
        }
    }
}
