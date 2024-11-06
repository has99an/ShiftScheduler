using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ShiftSchedulerAPI.Models;
using System;
using System.Collections.Generic;

namespace ShiftSchedulerAPI.DataAccess
{
    public class ShiftAccess : IShiftAccess
    {
        private readonly string _connectionString;

        public ShiftAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ShiftSchedulerConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }
        }

        public List<Shift> GetAllShifts()
        {
            List<Shift> foundShifts = new List<Shift>();

            try
            {
                string queryString = @"
                                        SELECT s.*, 
                                               e.FirstName + ' ' + e.LastName AS EmployeeFullName 
                                        FROM Shifts s
                                        LEFT JOIN Employees e ON s.EmployeeID = e.EmployeeID";


                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    con.Open();
                    SqlDataReader shiftReader = readCommand.ExecuteReader();

                    while (shiftReader.Read())
                    {
                        Shift shift = GetShiftFromReader(shiftReader);
                        foundShifts.Add(shift);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving shifts: {ex.Message}");
                throw;
            }

            return foundShifts;
        }

        public string GetEmployeeFullNameById(int employeeId)
        {
            string fullName = null;

            try
            {
                string query = "SELECT FirstName + ' ' + LastName FROM Employees WHERE EmployeeID = @EmployeeId";
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@EmployeeId", employeeId);

                    con.Open();
                    var result = command.ExecuteScalar();
                    fullName = result as string;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving employee full name: {ex.Message}");
                throw;
            }

            return fullName;
        }


        public List<Shift> GetShiftsByEmployeeId(int employeeId)
        {
            List<Shift> foundShifts = new List<Shift>();

            try
            {
                string queryString = "SELECT * FROM Shifts WHERE EmployeeID = @EmployeeId";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    readCommand.Parameters.AddWithValue("@EmployeeId", employeeId);

                    con.Open();
                    SqlDataReader shiftReader = readCommand.ExecuteReader();

                    while (shiftReader.Read())
                    {
                        Shift shift = GetShiftFromReader(shiftReader);
                        foundShifts.Add(shift);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving shifts by employee ID: {ex.Message}");
                throw;
            }

            return foundShifts;
        }

        public Shift GetShiftById(int id)
        {
            Shift foundShift = null;

            try
            {
                string queryString = @"
                                        SELECT s.*, 
                                               e.FirstName + ' ' + e.LastName AS EmployeeFullName 
                                        FROM Shifts s
                                        LEFT JOIN Employees e ON s.EmployeeID = e.EmployeeID
                                        WHERE s.ShiftID = @Id";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    readCommand.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    SqlDataReader reader = readCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        foundShift = GetShiftFromReader(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving shift by ID: {ex.Message}");
                throw;
            }

            return foundShift;
        }


        public int AddShift(Shift shift)
        {
            int insertedId = -1;

            try
            {
                string insertString = "INSERT INTO Shifts (StartTime, EndTime, Date" +
                                      (shift.EmployeeID.HasValue ? ", EmployeeID" : "") +
                                      ") OUTPUT INSERTED.ShiftID VALUES (@StartTime, @EndTime, @Date" +
                                      (shift.EmployeeID.HasValue ? ", @EmployeeID" : "") + ")";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand createCommand = new SqlCommand(insertString, con))
                {
                    createCommand.Parameters.AddWithValue("@StartTime", shift.StartTime);
                    createCommand.Parameters.AddWithValue("@EndTime", shift.EndTime);
                    createCommand.Parameters.AddWithValue("@Date", shift.Date);

                    if (shift.EmployeeID.HasValue)
                    {
                        createCommand.Parameters.AddWithValue("@EmployeeID", shift.EmployeeID);
                    }

                    con.Open();
                    insertedId = (int)createCommand.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding shift: {ex.Message}");
                throw;
            }

            return insertedId;
        }



        public void UpdateShift(Shift shift)
        {
            try
            {
                string updateString = "UPDATE Shifts SET EmployeeID = @EmployeeID, StartTime = @StartTime, EndTime = @EndTime, Date = @Date WHERE ShiftID = @ShiftID";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand updateCommand = new SqlCommand(updateString, con))
                {
                    updateCommand.Parameters.AddWithValue("@EmployeeID", (object)shift.EmployeeID ?? DBNull.Value);
                    updateCommand.Parameters.AddWithValue("@StartTime", shift.StartTime);
                    updateCommand.Parameters.AddWithValue("@EndTime", shift.EndTime);
                    updateCommand.Parameters.AddWithValue("@Date", shift.Date);
                    updateCommand.Parameters.AddWithValue("@ShiftID", shift.ShiftID);

                    con.Open();
                    updateCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating shift: {ex.Message}");
                throw;
            }
        }

        public void DeleteShift(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand command = con.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Shifts WHERE ShiftID = @Id";
                    command.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting shift: {ex.Message}");
                throw;
            }
        }

        private Shift GetShiftFromReader(SqlDataReader shiftReader)
        {
            int id = shiftReader.GetInt32(shiftReader.GetOrdinal("shiftID"));
            int? employeeID = shiftReader.IsDBNull(shiftReader.GetOrdinal("employeeID")) ? null : shiftReader.GetInt32(shiftReader.GetOrdinal("employeeID"));
            TimeSpan startTime = shiftReader.GetTimeSpan(shiftReader.GetOrdinal("startTime"));
            TimeSpan endTime = shiftReader.GetTimeSpan(shiftReader.GetOrdinal("endTime"));
            DateTime dateTime = shiftReader.GetDateTime(shiftReader.GetOrdinal("date"));
            DateOnly date = DateOnly.FromDateTime(dateTime);

            return new Shift
            {
                ShiftID = id,
                EmployeeID = employeeID,
                StartTime = startTime,
                EndTime = endTime,
                Date = date
            };
        }
    }
}
