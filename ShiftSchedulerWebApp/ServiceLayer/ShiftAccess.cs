using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShiftSchedulerWebApp.Models;

namespace ShiftSchedulerWebApp.ServiceLayer
{
    public class ShiftAccess : IShiftAccess
    {
        private readonly IServiceConnection _shiftService;
        private readonly string _serviceBaseUrl = "http://localhost:5189/api/Shifts";

        public ShiftAccess()
        {
            _shiftService = new ServiceConnection(_serviceBaseUrl);
        }

        public async Task<List<Shift>?> GetShifts()
        {
            List<Shift>? shifts = null;

            HttpResponseMessage? response = await _shiftService.CallServiceGet();
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                shifts = JsonConvert.DeserializeObject<List<Shift>>(jsonString);
            }

            return shifts;
        }

        public async Task<List<Shift>?> GetShiftsByEmployeeId(int employeeId)
        {
            List<Shift>? shifts = null;

            HttpResponseMessage? response = await _shiftService.CallServiceGet($"{employeeId}/shifts");
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                shifts = JsonConvert.DeserializeObject<List<Shift>>(jsonString);
            }

            return shifts;
        }
        public async Task<Shift?> GetShiftById(int shiftId)
        {
            Shift? shift = null;

            HttpResponseMessage? response = await _shiftService.GetById(shiftId.ToString());
            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                shift = JsonConvert.DeserializeObject<Shift>(jsonString);
            }

            return shift;
        }

        public async Task<bool> CreateShift(Shift shift)
        {
            var postJson = new StringContent(JsonConvert.SerializeObject(shift), Encoding.UTF8, "application/json");
            HttpResponseMessage? response = await _shiftService.CallServicePost(postJson);
            return response != null && response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateShift(Shift shift)
        {
            var putJson = new StringContent(JsonConvert.SerializeObject(shift), Encoding.UTF8, "application/json");
            _shiftService.UseUrl = $"{_serviceBaseUrl}/{shift.ShiftID}";
            HttpResponseMessage? response = await _shiftService.CallServicePut(putJson);
            return response != null && response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteShift(int shiftId)
        {
            HttpResponseMessage? response = await _shiftService.CallServiceDelete();
            return response != null && response.IsSuccessStatusCode;
        }

        public async Task<string?> GetEmployeeFullNameByEmployeeId(int employeeId)
        {
            string? employeeFullName = null;

            HttpResponseMessage? response = await _shiftService.CallServiceGet($"employees/{employeeId}/fullname");

            if (response != null && response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                employeeFullName = JsonConvert.DeserializeObject<string>(jsonString); 
            }

            return employeeFullName;
        }


    }
}
