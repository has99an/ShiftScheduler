using ShiftSchedulerAPI.Models;
using ShiftSchedulerAPI.DTO;

namespace ShiftSchedulerAPI.Converters
{
    public static class ShiftConverter
    {
        public static ShiftDTO ToDTO(Shift shift)
        {
            return new ShiftDTO
            {
                ID = shift.ID,
                EmployeeID = shift.EmployeeID,
                StartTime = shift.StartTime,
                EndTime = shift.EndTime,
                Type = shift.Type
            };
        }

        public static Shift ToModel(ShiftDTO shiftDto)
        {
            return new Shift
            {
                ID = shiftDto.ID,
                EmployeeID = shiftDto.EmployeeID,
                StartTime = shiftDto.StartTime,
                EndTime = shiftDto.EndTime,
                Type = shiftDto.Type
            };
        }
    }
}
