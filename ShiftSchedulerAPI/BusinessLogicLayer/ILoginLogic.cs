using ShiftSchedulerAPI.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShiftSchedulerAPI.BusinessLogicLayer
{
    public interface ILoginLogic
    {
        Task<EmployeeDTO> ValidateEmployee(string email, string password);
        Task<int> AddLogin(LoginDTO login); // Fjern salt som parameter
        Task<LoginDTO> GetLoginByEmail(string email);
        Task UpdateLogin(LoginDTO login); // Fjern salt som parameter
        Task RemoveLogin(string email);
    }
}
