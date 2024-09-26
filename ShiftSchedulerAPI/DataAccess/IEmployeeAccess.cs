using ShiftSchedulerAPI.Models;

namespace ShiftSchedulerAPI.DataAccess
{
    public interface IEmployeeAccess
    {
        Employee GetEmployeeById(int employeeId);
        List<Employee> GetAllEmployees();
        int AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int employeeId);
    }
}
