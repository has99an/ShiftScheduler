using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ShiftSchedulerAPI.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ShiftSchedulerAPI.DataAccess
{
    public class LoginAccess : ILoginAccess
    {
        private readonly string _connectionString;

        public LoginAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ShiftSchedulerConnection");
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Database connection string is not configured.");
            }
        }

        public Employee ValidateEmployee(string email, string password)
        {
            Employee foundEmployee = null;
            Login foundLogin = null;

            try
            {
                // Først, hent loginoplysninger fra Logins tabellen baseret på e-mail
                string loginQuery = "SELECT * FROM Logins WHERE Email = @Email";
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand loginCommand = new SqlCommand(loginQuery, con))
                {
                    loginCommand.Parameters.AddWithValue("@Email", email);

                    con.Open();
                    using (SqlDataReader loginReader = loginCommand.ExecuteReader())
                    {
                        if (loginReader.Read())
                        {
                            foundLogin = new Login
                            {
                                Email = loginReader.GetString(loginReader.GetOrdinal("Email")),
                                Password = loginReader.GetString(loginReader.GetOrdinal("Password")),
                                Salt = loginReader.GetString(loginReader.GetOrdinal("Salt"))
                            };
                        }
                    }
                }

                // Hvis loginoplysninger ikke findes, returner null
                if (foundLogin == null)
                {
                    return null;
                }

                // Valider det indtastede password ved at hashe det med det lagrede salt
                string hashedEnteredPassword = HashPassword(password, foundLogin.Salt);
                if (hashedEnteredPassword != foundLogin.Password)
                {
                    // Hvis adgangskoderne ikke matcher, returner null (forkert password)
                    return null;
                }

                // Hvis adgangskoden er korrekt, fortsæt med at hente medarbejderen fra Employees tabellen
                string employeeQuery = "SELECT * FROM Employees WHERE Mail = @Email";
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand employeeCommand = new SqlCommand(employeeQuery, con))
                {
                    employeeCommand.Parameters.AddWithValue("@Email", email);

                    con.Open();
                    using (SqlDataReader employeeReader = employeeCommand.ExecuteReader())
                    {
                        if (employeeReader.Read())
                        {
                            foundEmployee = new Employee
                            {
                                EmployeeID = employeeReader.GetInt32(employeeReader.GetOrdinal("EmployeeID")),
                                FirstName = employeeReader.GetString(employeeReader.GetOrdinal("FirstName")),
                                LastName = employeeReader.GetString(employeeReader.GetOrdinal("LastName")),
                                ZipCode = employeeReader.GetString(employeeReader.GetOrdinal("ZipCode")),
                                StreetName = employeeReader.GetString(employeeReader.GetOrdinal("StreetName")),
                                HouseNo = employeeReader.GetString(employeeReader.GetOrdinal("HouseNo")),
                                Mail = employeeReader.GetString(employeeReader.GetOrdinal("Mail")),
                                PhoneNumber = employeeReader.GetString(employeeReader.GetOrdinal("PhoneNumber")),
                                EmployeeType = (EmployeeType)Enum.Parse(typeof(EmployeeType), employeeReader.GetString(employeeReader.GetOrdinal("EmployeeType")))
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating employee: {ex.Message}");
                throw;
            }

            return foundEmployee;
        }

        public int AddLogin(Login login)
        {
            int insertedId = -1;

            try
            {
                string salt = GenerateSalt(); // Generer salt
                string hashedPassword = HashPassword(login.Password, salt); // Hash password med salt

                // Opdateret indsættelsesforespørgsel
                string insertString = "INSERT INTO Logins (Email, Password, Salt) VALUES (@Email, @Password, @Salt); SELECT SCOPE_IDENTITY();";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand createCommand = new SqlCommand(insertString, con))
                {
                    createCommand.Parameters.AddWithValue("@Email", login.Email);
                    createCommand.Parameters.AddWithValue("@Password", hashedPassword);
                    createCommand.Parameters.AddWithValue("@Salt", salt);

                    con.Open();
                    object result = createCommand.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        insertedId = Convert.ToInt32(result);
                    }
                    else
                    {
                        throw new Exception("Failed to insert login record; no ID was returned.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding login: {ex.Message}");
                throw;
            }

            return insertedId;
        }

        public Login GetLoginByEmail(string email)
        {
            Login foundLogin = null;

            try
            {
                string queryString = "SELECT * FROM Logins WHERE Email = @Email";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    readCommand.Parameters.AddWithValue("@Email", email);

                    con.Open();
                    using (SqlDataReader loginReader = readCommand.ExecuteReader())
                    {
                        if (loginReader.Read())
                        {
                            foundLogin = new Login
                            {
                                Email = loginReader.GetString(loginReader.GetOrdinal("Email")),
                                Password = loginReader.GetString(loginReader.GetOrdinal("Password")),
                                Salt = loginReader.GetString(loginReader.GetOrdinal("Salt")) // Hent salt fra databasen
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving login by email: {ex.Message}");
                throw;
            }

            return foundLogin;
        }

        public bool ValidatePassword(string enteredPassword, string storedHash, string storedSalt)
        {
            string hashedEnteredPassword = HashPassword(enteredPassword, storedSalt);
            return hashedEnteredPassword == storedHash;
        }

        public List<Login> GetAllLogins()
        {
            List<Login> logins = new List<Login>();

            try
            {
                string queryString = "SELECT * FROM Logins";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand readCommand = new SqlCommand(queryString, con))
                {
                    con.Open();
                    using (SqlDataReader loginReader = readCommand.ExecuteReader())
                    {
                        while (loginReader.Read())
                        {
                            logins.Add(new Login
                            {
                                Email = loginReader.GetString(loginReader.GetOrdinal("Email")),
                                Password = loginReader.GetString(loginReader.GetOrdinal("Password")),
                                Salt = loginReader.GetString(loginReader.GetOrdinal("Salt")) // Hent salt
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving logins: {ex.Message}");
                throw;
            }

            return logins;
        }

        public void UpdateLogin(Login login)
        {
            try
            {
                string hashedPassword = HashPassword(login.Password, login.Salt); // Brug salt til at hash password

                string updateString = "UPDATE Logins SET Password = @Password WHERE Email = @Email";

                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand updateCommand = new SqlCommand(updateString, con))
                {
                    updateCommand.Parameters.AddWithValue("@Email", login.Email);
                    updateCommand.Parameters.AddWithValue("@Password", hashedPassword);

                    con.Open();
                    updateCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating login: {ex.Message}");
                throw;
            }
        }

        public void DeleteLogin(string email)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                using (SqlCommand command = con.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Logins WHERE Email = @Email";
                    command.Parameters.AddWithValue("@Email", email);

                    con.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting login: {ex.Message}");
                throw;
            }
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = password + salt;
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
