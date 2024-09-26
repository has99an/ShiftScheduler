using ShiftSchedulerAPI.Models;

namespace ShiftSchedulerAPI.DTO
{
    public class ShiftDTO
    {
        public int ShiftID { get; set; }
        public int? EmployeeID { get; set; } 
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateOnly Date {  get; set; }
        public ShiftType Type { get; set; }
        public ShiftStatus Status { get; set; }

    }
}
