using System.Net.Http;
using System.Threading.Tasks;

namespace ShiftSchedulerMobile.Services
{
    public interface IApiServiceConnection
    {
        string BaseUrl { get; init; }
        string UseUrl { get; set; }

        Task<HttpResponseMessage> CallServiceGet();
        Task<HttpResponseMessage> CallServicePost(StringContent postJson);
        Task<HttpResponseMessage> CallServicePut(StringContent postJson);
        Task<HttpResponseMessage> CallServiceDelete();
        Task<HttpResponseMessage> GetById(int id);
        Task<HttpResponseMessage> GetById(string id);
        Task<HttpResponseMessage> Get(string url);
    }
}
