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
            string salt = GenerateSalt(); // Generer salt ved oprettelse af login
            await _loginLogic.AddLogin(login); // Send salt til metoden i Business Logic
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

        [HttpPut]
        public async Task<IActionResult> UpdateLogin([FromBody] LoginDTO login)
        {
            // Opdatering uden salt
            await _loginLogic.UpdateLogin(login); // Opdater login uden salt
            return NoContent();
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteLogin(string email)
        {
            await _loginLogic.RemoveLogin(email);
            return NoContent();
        }

        private string GenerateSalt() // Helper metode til at generere salt
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
