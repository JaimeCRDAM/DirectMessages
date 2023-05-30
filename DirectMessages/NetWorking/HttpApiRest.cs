using System.Text;
using System.Text.Json;

namespace DirectMessages.NetWorking
{
    public class HttpApiRest : IHttpApiRest
    {
        private readonly HttpClient _httpClient;

        public HttpApiRest(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SubscribeToChannel(List<Guid> id)
        {
            var response = await _httpClient.PostAsync($"http://172.17.0.3/api/subscribetochannel", null);
            if (response.IsSuccessStatusCode)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
