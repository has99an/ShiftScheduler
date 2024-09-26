using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ShiftSchedulerAPI.Models;
using System;

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
                string queryString = "SELECT * FROM Shifts WHERE ID = @Id";

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
                string insertString = "INSERT INTO Shifts (EmployeeID, StartTime, EndTime, Type) OUTPUT INSERTED.ID VALUES (@EmployeeID, @StartTime, @EndTime, @Type)";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand createCommand = new SqlCommand(insertString, con))
                {
                    createCommand.Parameters.AddWithValue("@EmployeeID", shift.EmployeeID);
                    createCommand.Parameters.AddWithValue("@StartTime", shift.StartTime);
                    createCommand.Parameters.AddWithValue("@EndTime", shift.EndTime);
                    createCommand.Parameters.AddWithValue("@Type", shift.Type);

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
                string updateString = "UPDATE Shifts SET EmployeeID = @EmployeeID, StartTime = @StartTime, EndTime = @EndTime, Type = @Type WHERE ID = @ID";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand updateCommand = new SqlCommand(updateString, con))
                {
                    updateCommand.Parameters.AddWithValue("@EmployeeID", shift.EmployeeID);
                    updateCommand.Parameters.AddWithValue("@StartTime", shift.StartTime);
                    updateCommand.Parameters.AddWithValue("@EndTime", shift.EndTime);
                    updateCommand.Parameters.AddWithValue("@Type", shift.Type);
                    updateCommand.Parameters.AddWithValue("@ID", shift.ID);

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
                    command.CommandText = "DELETE FROM Shifts WHERE ID = @Id";
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
            int id = shiftReader.GetInt32(shiftReader.GetOrdinal("ID"));
            string employeeID = shiftReader.GetString(shiftReader.GetOrdinal("EmployeeID"));
            DateTime startTime = shiftReader.GetDateTime(shiftReader.GetOrdinal("StartTime"));
            DateTime endTime = shiftReader.GetDateTime(shiftReader.GetOrdinal("EndTime"));
            ShiftType type = (ShiftType)shiftReader.GetInt32(shiftReader.GetOrdinal("Type"));

            return new Shift
            {
                ID = id,
                EmployeeID = employeeID,
                StartTime = startTime,
                EndTime = endTime,
                Type = type
            };
        }
    }
}
