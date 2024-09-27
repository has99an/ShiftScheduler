using System.Net.Http;
using System.Threading.Tasks;

namespace ShiftSchedulerMobile.Services
{
    public class ApiServiceConnection : IApiServiceConnection
    {
        private readonly HttpClient _httpClient;

        public ApiServiceConnection(string baseUrl)
        {
            _httpClient = new HttpClient();
            BaseUrl = baseUrl;
            UseUrl = BaseUrl;
        }

        public string BaseUrl { get; init; }
        public string UseUrl { get; set; }

        public async Task<HttpResponseMessage> CallServiceGet()
        {
            if (UseUrl != null)
            {
                return await _httpClient.GetAsync(UseUrl);
            }
            return null;
        }

        public async Task<HttpResponseMessage> CallServicePost(StringContent postJson)
        {
            if (UseUrl != null)
            {
                return await _httpClient.PostAsync(UseUrl, postJson);
            }
            return null;
        }

        public async Task<HttpResponseMessage> CallServicePut(StringContent postJson)
        {
            if (UseUrl != null)
            {
                return await _httpClient.PutAsync(UseUrl, postJson);
            }
            return null;
        }

        public async Task<HttpResponseMessage> CallServiceDelete()
        {
            if (UseUrl != null)
            {
                return await _httpClient.DeleteAsync(UseUrl);
            }
            return null;
        }

        public async Task<HttpResponseMessage> GetById(string id)
        {
            if (UseUrl != null)
            {
                UseUrl = $"{BaseUrl}{id}";
                return await _httpClient.GetAsync(UseUrl);
            }
            return null;
        }

        public async Task<HttpResponseMessage> GetById(int id)
        {
            if (UseUrl != null)
            {
                UseUrl = $"{BaseUrl}{id}";
                return await _httpClient.GetAsync(UseUrl);
            }
            return null;
        }

        public async Task<HttpResponseMessage> Get(string url)
        {
            if (UseUrl != null)
            {
                return await _httpClient.GetAsync(url);
            }
            return null;
        }
    }
}
