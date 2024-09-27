using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShiftSchedulerMobile.DTO;

namespace ShiftSchedulerMobile.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        private readonly string _serviceBaseUrl = "https://localhost:7113/api/Auth/";

        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EmployeeDTO> LoginAsync(string email, string password)
        {
            var loginDto = new LoginDTO { Email = email, Password = password };
            var content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_serviceBaseUrl + "login", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<EmployeeDTO>(jsonResult);
            }

            throw new HttpRequestException($"Error logging in: {response.StatusCode}");
        }

        public async Task<bool> RegisterAsync(LoginDTO login)
        {
            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_serviceBaseUrl, content);

            return response.IsSuccessStatusCode; // Returner success status
        }

        public async Task<LoginDTO> GetLoginByEmailAsync(string email)
        {
            var response = await _httpClient.GetAsync(_serviceBaseUrl + email);
            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<LoginDTO>(jsonResult);
            }

            return null; // Håndter fejl her
        }

        public async Task UpdateLoginAsync(LoginDTO login)
        {
            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_serviceBaseUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error updating login: {response.StatusCode}");
            }
        }

        public async Task RemoveLoginAsync(string email)
        {
            var response = await _httpClient.DeleteAsync(_serviceBaseUrl + email);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error removing login: {response.StatusCode}");
            }
        }
    }
}
