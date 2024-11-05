using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShiftSchedulerWebApp.Models;

namespace ShiftSchedulerWebApp.ServiceLayer
{
    public class EmployeeAccess : IEmployeeAccess
    {
        private readonly IServiceConnection _employeeService;
        private readonly string _serviceBaseUrl = "http://localhost:5189/api/Employee/";

        public EmployeeAccess()
        {
            _employeeService = new ServiceConnection(_serviceBaseUrl);
        }

        public async Task<List<Employee>?> GetEmployees()
        {
            List<Employee>? employees = null;

            HttpResponseMessage? response = await _employeeService.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                employees = JsonConvert.DeserializeObject<List<Employee>>(jsonString);
            }

            return employees;
        }

        public async Task<Employee?> GetEmployeeById(int employeeId)
        {
            Employee? employee = null;

            HttpResponseMessage? response = await _employeeService.GetById(employeeId.ToString());
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                employee = JsonConvert.DeserializeObject<Employee>(jsonString);
            }

            return employee;
        }

        public async Task<bool> CreateEmployee(Employee employee)
        {
            var postJson = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
            HttpResponseMessage? response = await _employeeService.CallServicePost(postJson);
            return response != null && response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            var putJson = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
            _employeeService.UseUrl = $"{_serviceBaseUrl}{employee.EmployeeID}";
            HttpResponseMessage? response = await _employeeService.CallServicePut(putJson);
            return response != null && response.IsSuccessStatusCode;
        }


        public async Task<bool> DeleteEmployee(int employeeId)
        {
            HttpResponseMessage? response = await _employeeService.CallServiceDelete();
            return response != null && response.IsSuccessStatusCode;
        }
    }
}
