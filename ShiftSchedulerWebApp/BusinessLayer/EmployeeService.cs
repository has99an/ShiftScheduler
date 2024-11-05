using System.Collections.Generic;
using System.Threading.Tasks;
using ShiftSchedulerWebApp.Models;
using ShiftSchedulerWebApp.ServiceLayer;

namespace ShiftSchedulerWebApp.BusinessLayer
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeAccess _employeeAccess;

        public EmployeeService(IEmployeeAccess employeeAccess)
        {
            _employeeAccess = employeeAccess;
        }

        public async Task<List<Employee>?> GetEmployees()
        {
            return await _employeeAccess.GetEmployees();
        }

        public async Task<Employee?> GetEmployeeById(int employeeId)
        {
            return await _employeeAccess.GetEmployeeById(employeeId);
        }

        public async Task<bool> CreateEmployee(Employee employee)
        {
            return await _employeeAccess.CreateEmployee(employee);
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            return await _employeeAccess.UpdateEmployee(employee);
        }

        public async Task<bool> DeleteEmployee(int employeeId)
        {
            return await _employeeAccess.DeleteEmployee(employeeId);
        }
    }
}
