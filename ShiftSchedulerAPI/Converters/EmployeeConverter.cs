using ShiftSchedulerAPI.Models;
using ShiftSchedulerAPI.DTO;
using System.Collections.Generic;
using System.Linq;

namespace ShiftSchedulerAPI.Converters
{
    public static class EmployeeConverter
    {
        public static EmployeeDTO ToDTO(Employee employee)
        {
            return new EmployeeDTO
            {
                EmployeeID = employee.EmployeeID,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Mail = employee.Mail,
                PhoneNumber = employee.PhoneNumber,
                StreetName = employee.StreetName,
                ZipCode = employee.ZipCode,
                HouseNo = employee.HouseNo,
                EmployeeType = employee.EmployeeType
            };
        }

        public static Employee ToModel(EmployeeDTO employeeDto)
        {
            return new Employee
            {
                EmployeeID = employeeDto.EmployeeID,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Mail = employeeDto.Mail,
                PhoneNumber = employeeDto.PhoneNumber,
                StreetName = employeeDto.StreetName,
                ZipCode = employeeDto.ZipCode,
                HouseNo = employeeDto.HouseNo,
                EmployeeType = employeeDto.EmployeeType
            };
        }

        public static List<EmployeeDTO> ToDTOCollection(List<Employee> employees)
        {
            return employees.Select(e => ToDTO(e)).ToList();
        }
    }
}
