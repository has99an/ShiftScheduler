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
                string queryString = "SELECT * FROM Shifts";

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

        public Shift GetShiftById(int id)
        {
            Shift foundShift = null;

            try
            {
                string queryString = "SELECT * FROM Shifts WHERE ShiftID = @Id";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    readCommand.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    using (SqlDataReader shiftReader = readCommand.ExecuteReader())
                    {
                        if (shiftReader.Read())
                        {
                            foundShift = GetShiftFromReader(shiftReader);
                        }
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
                string insertString = "INSERT INTO Shifts (EmployeeID, StartTime, EndTime, Date, Type, Status) OUTPUT INSERTED.ShiftID VALUES (@EmployeeID, @StartTime, @EndTime, @Date, @Type, @Status)";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand createCommand = new SqlCommand(insertString, con))
                {
                    createCommand.Parameters.AddWithValue("@EmployeeID", shift.EmployeeID);
                    createCommand.Parameters.AddWithValue("@StartTime", shift.StartTime);
                    createCommand.Parameters.AddWithValue("@EndTime", shift.EndTime);
                    createCommand.Parameters.AddWithValue("@Date", shift.Date);
                    createCommand.Parameters.AddWithValue("@Type", shift.Type);
                    createCommand.Parameters.AddWithValue("@Status", shift.Status);

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
                string updateString = "UPDATE Shifts SET EmployeeID = @EmployeeID, StartTime = @StartTime, EndTime = @EndTime, Date = @Date, Type = @Type, Status = @Status WHERE ShiftID = @ShiftID";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand updateCommand = new SqlCommand(updateString, con))
                {
                    updateCommand.Parameters.AddWithValue("@EmployeeID", shift.EmployeeID);
                    updateCommand.Parameters.AddWithValue("@StartTime", shift.StartTime);
                    updateCommand.Parameters.AddWithValue("@EndTime", shift.EndTime);
                    updateCommand.Parameters.AddWithValue("@Date", shift.Date);
                    updateCommand.Parameters.AddWithValue("@Type", shift.Type);
                    updateCommand.Parameters.AddWithValue("@Status", shift.Status);
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
            ShiftType type = (ShiftType)Enum.Parse(typeof(ShiftType), shiftReader.GetString(shiftReader.GetOrdinal("type")));
            ShiftStatus status = (ShiftStatus)Enum.Parse(typeof(ShiftStatus), shiftReader.GetString(shiftReader.GetOrdinal("status")));

            return new Shift
            {
                ShiftID = id,
                EmployeeID = employeeID,
                StartTime = startTime,
                EndTime = endTime,
                Date = date,
                Type = type,
                Status = status
            };
        }
    }
}
