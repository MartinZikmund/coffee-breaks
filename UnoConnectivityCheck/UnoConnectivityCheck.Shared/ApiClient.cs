using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace UnoConnectivityCheck
{
    internal class ApiClient
    {
        private readonly HttpClient _client = new();

        public async Task<string> GetDataAsync()
        {
            var response = await _client.GetAsync("https://run.mocky.io/v3/31b38cff-bbe8-4c53-853d-7978422ca3b5");
            if (response.IsSuccessStatusCode)
            {
                return DateTimeOffset.Now.ToString("HH:mm:ss");
            }
            else
            {
                return "Exception: API is unreachable";
            }
        }
    }
}
