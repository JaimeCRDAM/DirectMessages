using GenericTools;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace DirectMessages.NetWorking
{
    public class HttpApiRest : IHttpApiRest
    {
        private readonly HttpClient _httpClient;
        private readonly FireBase _firebase;

        public HttpApiRest(HttpClient httpClient, FireBase firebase)
        {
            _httpClient = httpClient;
            _firebase = firebase;
        }

        public async Task<bool> SubscribeToChannel(List<Guid> id, Guid channelid)
        {
            //serialize id to json
            var json = JsonSerializer.Serialize(id);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{IpsEnum.LogUpIn}api/subscribetochannel/{channelid}", content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public async Task<string> GetUserNameById(Guid id)
        {
            var response = await _httpClient.GetAsync($"{IpsEnum.LogUpIn}api/getusername/{id}");
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
    }
}
