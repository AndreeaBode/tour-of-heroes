using Microsoft.AspNetCore.Mvc;
using HeroAPI.BusinessLogicLayer;
using HeroAPI.DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace HeroAPI.PresentationLayer
{
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public LoginController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(Register model)
        {
            var user = await _userService.RegisterAsync(model); 
            if (user == null)
                return BadRequest("Registration failed.");

            return Ok("Registration successful.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(Login model)
        {
            var user = await _userService.LoginAsync(model); 
            if (user == null)
                return Unauthorized();

            var token = _userService.GenerateJwtToken(user, _configuration["Jwt:Key"], _configuration["Jwt:Issuer"]);
            return Ok(new { token });
        }
    }
}
