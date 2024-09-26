using ShiftSchedulerAPI.DataAccess;
using ShiftSchedulerAPI.DTO;
using ShiftSchedulerAPI.Models;
using ShiftSchedulerAPI.Converters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShiftSchedulerAPI.BusinessLogicLayer
{
    public class EmployeeLogic : IEmployeeLogic
    {
        private readonly IEmployeeAccess _employeeAccess;

        public EmployeeLogic(IEmployeeAccess employeeAccess)
        {
            _employeeAccess = employeeAccess;
        }

        public async Task<EmployeeDTO> GetEmployeeById(int employeeId)
        {
            try
            {
                Employee employee = await Task.Run(() => _employeeAccess.GetEmployeeById(employeeId));
                return EmployeeConverter.ToDTO(employee);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting employee by ID: {ex.Message}");
                throw;
            }
        }

        public async Task<List<EmployeeDTO>> GetAllEmployees()
        {
            try
            {
                List<Employee> employees = await Task.Run(() => _employeeAccess.GetAllEmployees());
                return EmployeeConverter.ToDTOCollection(employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all employees: {ex.Message}");
                throw;
            }
        }

        public async Task<int> AddEmployee(EmployeeDTO newEmployee)
        {
            try
            {
                Employee employee = EmployeeConverter.ToModel(newEmployee);
                int newEmployeeID = await Task.Run(() => _employeeAccess.AddEmployee(employee));
                return newEmployeeID;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding employee: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateEmployee(EmployeeDTO updatedEmployee)
        {
            try
            {
                Employee employee = EmployeeConverter.ToModel(updatedEmployee);
                await Task.Run(() => _employeeAccess.UpdateEmployee(employee));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating employee: {ex.Message}");
                throw;
            }
        }

        public async Task RemoveEmployee(int employeeId)
        {
            try
            {
                await Task.Run(() => _employeeAccess.DeleteEmployee(employeeId));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing employee: {ex.Message}");
                throw;
            }
        }
    }
}
