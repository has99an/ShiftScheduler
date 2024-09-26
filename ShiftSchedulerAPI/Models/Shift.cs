namespace ShiftSchedulerAPI.Models
{
    public class Shift
    {
        public int ID { get; set; }
        public string EmployeeID { get; set; } 
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ShiftType Type { get; set; } 
    }

}
