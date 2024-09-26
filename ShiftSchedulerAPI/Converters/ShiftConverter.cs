using ShiftSchedulerAPI.DTO;
using ShiftSchedulerAPI.Models;
using System.Collections.Generic;

namespace ShiftSchedulerAPI.Converters
{
    public static class ShiftConverter
    {
        public static ShiftDTO ToDTO(Shift shift)
        {
            return new ShiftDTO
            {
                ShiftID = shift.ShiftID,
                EmployeeID = shift.EmployeeID,
                StartTime = shift.StartTime,
                EndTime = shift.EndTime,
                Date = shift.Date,
                Type = shift.Type,
                Status = shift.Status
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
                Date = shiftDto.Date,
                Type = shiftDto.Type,
                Status = shiftDto.Status
            };
        }

        public static List<ShiftDTO> ToDTOCollection(List<Shift> shifts)
        {
            var shiftDtos = new List<ShiftDTO>();
            foreach (var shift in shifts)
            {
                shiftDtos.Add(ToDTO(shift));
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
