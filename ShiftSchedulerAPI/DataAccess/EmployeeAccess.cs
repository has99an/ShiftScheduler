using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ShiftSchedulerAPI.Models;
using System;
using System.Collections.Generic;

namespace ShiftSchedulerAPI.DataAccess
{
    public class EmployeeAccess : IEmployeeAccess
    {
        private readonly string _connectionString;

        public EmployeeAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ShiftSchedulerConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> foundEmployees = new List<Employee>();

            try
            {
                string queryString = "SELECT * FROM Employees";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    con.Open();
                    SqlDataReader employeeReader = readCommand.ExecuteReader();

                    while (employeeReader.Read())
                    {
                        Employee employee = GetEmployeeFromReader(employeeReader);
                        foundEmployees.Add(employee);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving employees: {ex.Message}");
                throw;
            }

            return foundEmployees;
        }

        public Employee GetEmployeeById(int employeeId)
        {
            Employee foundEmployee = null;

            try
            {
                string queryString = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeId";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    readCommand.Parameters.AddWithValue("@EmployeeId", employeeId);

                    con.Open();
                    using (SqlDataReader employeeReader = readCommand.ExecuteReader())
                    {
                        if (employeeReader.Read())
                        {
                            foundEmployee = GetEmployeeFromReader(employeeReader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving employee by ID: {ex.Message}");
                throw;
            }

            return foundEmployee;
        }

        public int AddEmployee(Employee employee)
        {
            int insertedId = -1;

            try
            {
                string insertString = "INSERT INTO Employees (FirstName, LastName, ZipCode, StreetName, HouseNo, Mail, PhoneNumber) OUTPUT INSERTED.EmployeeID VALUES (@FirstName, @LastName, @ZipCode, @StreetName, @HouseNo, @Mail, @PhoneNumber)";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand createCommand = new SqlCommand(insertString, con))
                {
                    createCommand.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    createCommand.Parameters.AddWithValue("@LastName", employee.LastName);
                    createCommand.Parameters.AddWithValue("@ZipCode", employee.ZipCode);
                    createCommand.Parameters.AddWithValue("@StreetName", employee.StreetName);
                    createCommand.Parameters.AddWithValue("@HouseNo", employee.HouseNo);
                    createCommand.Parameters.AddWithValue("@Mail", employee.Mail);
                    createCommand.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);

                    con.Open();
                    insertedId = (int)createCommand.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding employee: {ex.Message}");
                throw;
            }

            return insertedId;
        }

        public void UpdateEmployee(Employee employee)
        {
            try
            {
                string updateString = "UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, ZipCode = @ZipCode, StreetName = @StreetName, HouseNo = @HouseNo, Mail = @Mail, PhoneNumber = @PhoneNumber WHERE EmployeeID = @EmployeeID";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand updateCommand = new SqlCommand(updateString, con))
                {
                    updateCommand.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    updateCommand.Parameters.AddWithValue("@LastName", employee.LastName);
                    updateCommand.Parameters.AddWithValue("@ZipCode", employee.ZipCode);
                    updateCommand.Parameters.AddWithValue("@StreetName", employee.StreetName);
                    updateCommand.Parameters.AddWithValue("@HouseNo", employee.HouseNo);
                    updateCommand.Parameters.AddWithValue("@Mail", employee.Mail);
                    updateCommand.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                    updateCommand.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);

                    con.Open();
                    updateCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating employee: {ex.Message}");
                throw;
            }
        }

        public void DeleteEmployee(int employeeId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand command = con.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Employees WHERE EmployeeID = @EmployeeId";
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);

                    con.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting employee: {ex.Message}");
                throw;
            }
        }

        private Employee GetEmployeeFromReader(SqlDataReader employeeReader)
        {
            int employeeId = employeeReader.GetInt32(employeeReader.GetOrdinal("employeeID"));
            string firstName = employeeReader.GetString(employeeReader.GetOrdinal("firstName"));
            string lastName = employeeReader.GetString(employeeReader.GetOrdinal("lastName"));
            string zipCode = employeeReader.GetString(employeeReader.GetOrdinal("zipCode"));
            string streetName = employeeReader.GetString(employeeReader.GetOrdinal("streetName"));
            string houseNo = employeeReader.GetString(employeeReader.GetOrdinal("houseNo"));
            string mail = employeeReader.GetString(employeeReader.GetOrdinal("mail"));
            string phoneNumber = employeeReader.GetString(employeeReader.GetOrdinal("phoneNumber"));

            return new Employee
            {
                EmployeeID = employeeId,
                FirstName = firstName,
                LastName = lastName,
                ZipCode = zipCode,
                StreetName = streetName,
                HouseNo = houseNo,
                Mail = mail,
                PhoneNumber = phoneNumber
            };
        }
    }
}
