using System.Collections.Generic;
using System.Threading.Tasks;
using ShiftSchedulerWebApp.Models;

namespace ShiftSchedulerWebApp.BusinessLayer
{
    public interface IEmployeeService
    {
        Task<List<Employee>?> GetEmployees();
        Task<Employee?> GetEmployeeById(int employeeId);
        Task<bool> CreateEmployee(Employee employee);
        Task<bool> UpdateEmployee(Employee employee);
        Task<bool> DeleteEmployee(int employeeId);
    }
}
