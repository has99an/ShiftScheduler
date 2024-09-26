using ShiftSchedulerAPI.Models;
using ShiftSchedulerAPI.DTO;

namespace ShiftSchedulerAPI.Converters
{
    public static class LoginConverter
    {
        public static LoginDTO ToDTO(Login login)
        {
            return new LoginDTO
            {
                Email = login.Email,
                Password = login.Password
    };
        }

        public static Login ToModel(LoginDTO loginDto)
        {
            return new Login
            {
                Email = loginDto.Email,
                Password = loginDto.Password
            };
        }

        public static List<LoginDTO> ToDTOCollection(List<Login> logins)
        {
            return logins.Select(l => ToDTO(l)).ToList();
        }
    }
}
