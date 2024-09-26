namespace ShiftSchedulerAPI.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; } // Unik ID for medarbejderen
        public string FirstName { get; set; } // Fornavn
        public string LastName { get; set; } // Efternavn
        public string ZipCode { get; set; } // Postnummer
        public string StreetName { get; set; } // Vejnavn
        public string HouseNo { get; set; } // Husnummer
        public string Mail { get; set; } // Email
        public string PhoneNumber { get; set; } // Telefonnummer
    }
}
