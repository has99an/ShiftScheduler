namespace ShiftSchedulerWebApp.ViewModels
{
    public class ShiftViewModel
    {
        public int ShiftID { get; set; }
        public int? EmployeeID { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateOnly Date { get; set; }
        public string EmployeeFullName { get; set; } 
    }

}
