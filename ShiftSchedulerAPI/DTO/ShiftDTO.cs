using ShiftSchedulerAPI.Models;

namespace ShiftSchedulerAPI.DTO
{
    public class ShiftDTO
    {
        public int ID { get; set; }
        public string EmployeeID { get; set; } // Kan være null for åbne vagter
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ShiftType Type { get; set; }
    }
}
