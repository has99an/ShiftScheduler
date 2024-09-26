using ShiftSchedulerAPI.Models;
using System.Collections.Generic;

namespace ShiftSchedulerAPI.DataAccess
{
    public interface ILoginAccess
    {
        Employee ValidateEmployee(string email, string password);
        int AddLogin(Login login);
        Login GetLoginByEmail(string email);
        List<Login> GetAllLogins();
        void UpdateLogin(Login login);
        void DeleteLogin(string email);
    }
}
