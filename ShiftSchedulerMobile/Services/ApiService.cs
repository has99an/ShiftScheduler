using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShiftSchedulerMobile.Models; // Erstat med dit namespace
using ShiftSchedulerMobile.DTO;

namespace ShiftSchedulerMobile.Services
{
    internal class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7113/api/") // Erstat med din API-URL
            };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Employee> LoginAsync(string email, string password)
        {
            var loginDto = new LoginDTO { Email = email, Password = password };
            var content = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("auth/login", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Employee>(jsonResult);
            }

            return null; // Håndter fejl her
        }

        // Flere metoder til at interagere med API'en kan tilføjes her
    }
}
