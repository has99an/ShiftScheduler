using ShiftSchedulerAPI.Models;
using ShiftSchedulerAPI.DataAccess;

namespace ShiftSchedulerAPI.BusinessLogicLayer
{
    public class EmployeeLogic
    {
        private readonly IEmployeeAccess _employeeAccess;

        public EmployeeLogic(IEmployeeAccess employeeAccess)
        {
            _employeeAccess = employeeAccess ?? throw new ArgumentNullException(nameof(employeeAccess));
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await Task.Run(() => _employeeAccess.GetAllEmployees());
        }

        public async Task<Employee> GetEmployeeById(int employeeId)
        {
            return await Task.Run(() => _employeeAccess.GetEmployeeById(employeeId));
        }

        public async Task<int> AddEmployee(Employee employee)
        {
            return await Task.Run(() => _employeeAccess.AddEmployee(employee));
        }

        public async Task UpdateEmployee(Employee employee)
        {
            await Task.Run(() => _employeeAccess.UpdateEmployee(employee));
        }

        public async Task DeleteEmployee(int employeeId)
        {
            await Task.Run(() => _employeeAccess.DeleteEmployee(employeeId));
        }
    }
}
