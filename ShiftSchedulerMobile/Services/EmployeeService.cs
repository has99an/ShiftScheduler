using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShiftSchedulerMobile.DTO;

namespace ShiftSchedulerMobile.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _serviceBaseUrl = "https://localhost:7113/api/Employee/";

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<EmployeeDTO>> GetAllEmployeesAsync()
        {
            var response = await _httpClient.GetAsync(_serviceBaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<EmployeeDTO>>(jsonResult);
            }

            return null; // Håndter fejl her
        }

        public async Task<EmployeeDTO> GetEmployeeByIdAsync(int employeeId)
        {
            var response = await _httpClient.GetAsync($"{_serviceBaseUrl}{employeeId}");
            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<EmployeeDTO>(jsonResult);
            }

            return null; // Håndter fejl her
        }

        public async Task<bool> AddEmployeeAsync(EmployeeDTO employee)
        {
            var content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_serviceBaseUrl, content);

            return response.IsSuccessStatusCode; // Returner success status
        }

        public async Task UpdateEmployeeAsync(EmployeeDTO employee)
        {
            var content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync(_serviceBaseUrl, content);
        }

        public async Task RemoveEmployeeAsync(int employeeId)
        {
            await _httpClient.DeleteAsync($"{_serviceBaseUrl}{employeeId}");
        }
    }
}
