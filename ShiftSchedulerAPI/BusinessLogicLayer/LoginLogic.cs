using ShiftSchedulerAPI.DataAccess;
using ShiftSchedulerAPI.DTO;
using ShiftSchedulerAPI.Models;
using ShiftSchedulerAPI.Converters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShiftSchedulerAPI.BusinessLogicLayer
{
    public class LoginLogic : ILoginLogic
    {
        private readonly ILoginAccess _loginAccess;

        public LoginLogic(ILoginAccess loginAccess)
        {
            _loginAccess = loginAccess;
        }

        public async Task<EmployeeDTO> ValidateEmployee(string email, string password)
        {
            try
            {
                Employee employee = await Task.Run(() => _loginAccess.ValidateEmployee(email, password));
                return EmployeeConverter.ToDTO(employee);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating employee: {ex.Message}");
                throw;
            }
        }

        public async Task<int> AddLogin(LoginDTO newLogin) // Fjern salt
        {
            try
            {
                Login login = LoginConverter.ToModel(newLogin);
                int newLoginID = await Task.Run(() => _loginAccess.AddLogin(login)); // Ingen salt parameter
                return newLoginID;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding login: {ex.Message}");
                throw;
            }
        }

        public async Task<LoginDTO> GetLoginByEmail(string email)
        {
            try
            {
                Login login = await Task.Run(() => _loginAccess.GetLoginByEmail(email));
                return LoginConverter.ToDTO(login);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting login by email: {ex.Message}");
                throw;
            }
        }

        public async Task<List<LoginDTO>> GetAllLogins()
        {
            try
            {
                List<Login> logins = await Task.Run(() => _loginAccess.GetAllLogins());
                return LoginConverter.ToDTOCollection(logins);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all logins: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateLogin(LoginDTO updatedLogin) // Fjern salt
        {
            try
            {
                Login login = LoginConverter.ToModel(updatedLogin);
                await Task.Run(() => _loginAccess.UpdateLogin(login)); // Ingen salt parameter
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating login: {ex.Message}");
                throw;
            }
        }

        public async Task RemoveLogin(string email)
        {
            try
            {
                await Task.Run(() => _loginAccess.DeleteLogin(email));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing login: {ex.Message}");
                throw;
            }
        }
    }
}
