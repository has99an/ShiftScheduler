using ShiftSchedulerWebApp.Models;

namespace ShiftSchedulerWebApp.ServiceLayer
{
    public interface IEmployeeAccess
    {
        Task<List<Employee>?> GetEmployees();
        Task<Employee?> GetEmployeeById(int employeeId);
        Task<bool> CreateEmployee(Employee employee);
        Task<bool> UpdateEmployee(Employee employee);
        Task<bool> DeleteEmployee(int employeeId);
    }
}
