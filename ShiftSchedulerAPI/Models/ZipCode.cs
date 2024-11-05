namespace ShiftSchedulerAPI.Models
{
    public class ZipCode
    {
        public int Code { get; set; } 
        public string City { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
