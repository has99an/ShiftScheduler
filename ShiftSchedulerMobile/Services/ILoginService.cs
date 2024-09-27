using ShiftSchedulerMobile.DTO;
using System.Threading.Tasks;

namespace ShiftSchedulerMobile.Services
{
    public interface ILoginService
    {
        Task<EmployeeDTO> LoginAsync(string email, string password);
        Task<bool> RegisterAsync(LoginDTO login);
        Task<LoginDTO> GetLoginByEmailAsync(string email);
        Task UpdateLoginAsync(LoginDTO login);
        Task RemoveLoginAsync(string email);
    }
}
