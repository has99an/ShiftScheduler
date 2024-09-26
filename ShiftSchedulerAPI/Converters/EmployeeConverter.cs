using ShiftSchedulerAPI.Models;
using ShiftSchedulerAPI.DTO;

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
                ZipCode = employee.ZipCode,
                StreetName = employee.StreetName,
                HouseNo = employee.HouseNo,
                Mail = employee.Mail,
                PhoneNumber = employee.PhoneNumber
            };
        }

        public static Employee ToModel(EmployeeDTO employeeDto)
        {
            return new Employee
            {
                EmployeeID = employeeDto.EmployeeID,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                ZipCode = employeeDto.ZipCode,
                StreetName = employeeDto.StreetName,
                HouseNo = employeeDto.HouseNo,
                Mail = employeeDto.Mail,
                PhoneNumber = employeeDto.PhoneNumber
            };
        }
    }
}
