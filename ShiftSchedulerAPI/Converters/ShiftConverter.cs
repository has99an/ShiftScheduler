using ShiftSchedulerAPI.DTO;
using ShiftSchedulerAPI.Models;
using System.Collections.Generic;

namespace ShiftSchedulerAPI.Converters
{
    public static class ShiftConverter
    {
        public static ShiftDTO ToDTO(Shift shift, string employeeFullName)
        {
            return new ShiftDTO
            {
                ShiftID = shift.ShiftID,
                EmployeeID = shift.EmployeeID,
                EmployeeFullName = string.IsNullOrEmpty(employeeFullName) ? "Open" : employeeFullName, 
                StartTime = shift.StartTime,
                EndTime = shift.EndTime,
                Date = shift.Date
            };
        }


        public static Shift ToModel(ShiftDTO shiftDto)
        {
            return new Shift
            {
                ShiftID = shiftDto.ShiftID,
                EmployeeID = shiftDto.EmployeeID,
                StartTime = shiftDto.StartTime,
                EndTime = shiftDto.EndTime,
                Date = shiftDto.Date
            };
        }

        public static List<ShiftDTO> ToDTOCollection(List<Shift> shifts, string employeeFullName)
        {
            var shiftDtos = new List<ShiftDTO>();
            foreach (var shift in shifts)
            {
                shiftDtos.Add(ToDTO(shift, employeeFullName)); 
            }
            return shiftDtos;
        }

        public static List<Shift> ToModelCollection(List<ShiftDTO> shiftDtos)
        {
            var shifts = new List<Shift>();
            foreach (var shiftDto in shiftDtos)
            {
                shifts.Add(ToModel(shiftDto));
            }
            return shifts;
        }
    }
}
