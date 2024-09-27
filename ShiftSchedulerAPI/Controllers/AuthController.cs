using Microsoft.AspNetCore.Mvc;
using ShiftSchedulerAPI.DTO;
using ShiftSchedulerAPI.BusinessLogicLayer;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ShiftSchedulerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginLogic _loginLogic;

        public AuthController(ILoginLogic loginLogic)
        {
            _loginLogic = loginLogic;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var employee = await _loginLogic.ValidateEmployee(loginDto.Email, loginDto.Password);
            if (employee == null)
            {
                return Unauthorized();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddLogin([FromBody] LoginDTO login)
        {
            string salt = GenerateSalt(); 
            await _loginLogic.AddLogin(login); 
            return CreatedAtAction(nameof(Login), new { email = login.Email }, login);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetLoginByEmail(string email)
        {
            var login = await _loginLogic.GetLoginByEmail(email);
            if (login == null)
            {
                return NotFound();
            }
            return Ok(login);
        }

        [HttpGet] 
        public async Task<IActionResult> GetAllLogins()
        {
            var logins = await _loginLogic.GetAllLogins();
            return Ok(logins);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateLogin([FromBody] LoginDTO login)
        {
            await _loginLogic.UpdateLogin(login); 
            return NoContent();
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteLogin(string email)
        {
            await _loginLogic.RemoveLogin(email);
            return NoContent();
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
    }
}
