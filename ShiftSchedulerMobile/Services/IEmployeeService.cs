using ShiftSchedulerMobile.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShiftSchedulerMobile.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetAllEmployeesAsync();
        Task<EmployeeDTO> GetEmployeeByIdAsync(int employeeId);
        Task<bool> AddEmployeeAsync(EmployeeDTO employee);
        Task UpdateEmployeeAsync(EmployeeDTO employee);
        Task RemoveEmployeeAsync(int employeeId);
    }
}
