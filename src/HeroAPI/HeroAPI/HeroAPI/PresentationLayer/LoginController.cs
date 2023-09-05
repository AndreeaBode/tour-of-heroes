using Microsoft.AspNetCore.Mvc;
using HeroAPI.BusinessLogicLayer;
using HeroAPI.DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using HeroAPI.BusinessLogicLayer.DTOs;

namespace HeroAPI.PresentationLayer
{
    /// <summary>
    /// Controller responsible for user authentication and registration operations.
    /// </summary>
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

        /// <summary>
        /// Registers a new user with the provided registration data.
        /// </summary>
        /// <param name="model">The registration data.</param>
        /// <returns>
        /// An HTTP response indicating success with a JWT token for the registered user,
        /// or a bad request response if registration fails.
        /// </returns>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO model)
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

            var tokenResponse = new LoginResponseDTO
            {
                Token = token
            };

            return Ok(
                new { 
                tokenResponse,
                message = "Registration successful." });
        }

        /// <summary>
        /// Authenticates a user with the provided login data.
        /// </summary>
        /// <param name="model">The login data.</param>
        /// <returns>
        /// An HTTP response indicating success with a JWT token for the authenticated user,
        /// or a not found response if authentication fails.
        /// </returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO model)
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

            var tokenResponse = new LoginResponseDTO
            {
                Token = token
            };

            return Ok(tokenResponse);
        }

    }
}
