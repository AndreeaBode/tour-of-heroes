using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HeroAPI.DataAccessLayer.Models;
using HeroAPI.DataAccessLayer.Repository;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;

namespace HeroAPI.BusinessLogicLayer
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository; 
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<User> RegisterAsync(Register model)
        {
            if (model.Password != model.ConfirmedPassword)
            {
                throw new Exception("Passwords do not match.");
            }

            var existingUser = await _userRepository.GetUserByEmailAsync(model.Email);

            if (existingUser != null)
            {
                throw new Exception("A user with this email already exists.");
            }

            var user = new User
            {
                Email = model.Email,
                Password = HashPassword(model.Password) 
            };

            await _userRepository.AddUserAsync(user);

            return user;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public async Task<User> LoginAsync(Login model)
        {
            
            var user = await _userRepository.GetUserByEmailAsync(model.Email);

            
            if (user != null && VerifyPassword(model.Password, user.Password))
            {
                return user;
            }

            return null;
        }

        public string GenerateJwtToken(User user, string key, string issuer)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var keyBytes = Encoding.UTF8.GetBytes(key);
            var keyCredentials = new SymmetricSecurityKey(keyBytes);
            var signInCredentials = new SigningCredentials(keyCredentials, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = signInCredentials,
                Issuer = issuer,
                Audience = issuer
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        
        private bool VerifyPassword(string password, string hashedPassword)
        {
            return password == hashedPassword;
        }
    }
}
