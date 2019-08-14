using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetCoreTest201908.Model
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient();
        }

        public async Task<bool> IsAuthAsync()
        {
            var httpResponseMessage = await _httpClient.GetAsync("http://abc.com/api/isauth");

            httpResponseMessage.EnsureSuccessStatusCode();

            var result = await httpResponseMessage.Content.ReadAsStringAsync();
            return result == "true";
        }
    }
}