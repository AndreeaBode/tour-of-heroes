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

        public LoginController(
            IUserService userService, 
            IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(Register model)
        {
            var user = await _userService.RegisterAsync(model);
            if (user == null)
            {
                return BadRequest("Registration failed.");
            }

            var token = _userService.GenerateJwtToken(
                user,
                _configuration["Jwt:Key"], 
                _configuration["Jwt:Issuer"]);

            var tokenResponse = new TokenResponse
            {
                Token = token
            };

            return Ok(
                new { 
                tokenResponse,
                message = "Registration successful." });
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(Login model)
        {
            var user = await _userService.LoginAsync(model);

            if (user == null)
            {
                return NotFound();
            }

            var token = _userService.
                GenerateJwtToken(
                user,
                _configuration["Jwt:Key"], 
                _configuration["Jwt:Issuer"]);

            var tokenResponse = new TokenResponse
            {
                Token = token
            };

            return Ok(tokenResponse);
        }

    }
}
