using ShiftSchedulerAPI.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShiftSchedulerAPI.BusinessLogicLayer
{
    public interface IEmployeeLogic
    {
        Task<List<EmployeeDTO>> GetAllEmployees();
        Task<EmployeeDTO> GetEmployeeById(int employeeId);
        Task<int> AddEmployee(EmployeeDTO employee);
        Task UpdateEmployee(EmployeeDTO employee);
        Task RemoveEmployee(int employeeId); // Rename to RemoveEmployee for consistency
    }
}
