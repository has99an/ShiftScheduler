namespace ShiftSchedulerWebApp.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ZipCode { get; set; } 
        public String? City { get; set; }
        public string StreetName { get; set; }
        public string HouseNo { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
}
