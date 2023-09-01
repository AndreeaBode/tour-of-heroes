using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HeroAPI.DataAccessLayer.Models;
using HeroAPI.DataAccessLayer.Repository;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Authentication;

namespace HeroAPI.BusinessLogicLayer
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(
            IUserRepository userRepository,
            IConfiguration configuration
            )
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<User?> RegisterAsync(Register model)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(model.Email);

            if (existingUser != null)
            {
                throw new Exception("A user with this email already exists.");
            }

            if (model.Password != model.ConfirmedPassword)
            {
                throw new Exception("Passwords do not match.");
            }

            var user = new User
            {
                Name = "Mari",
                Email = model.Email,
                Password = HashPassword(model.Password),
                IDHero = 24
            };

            await _userRepository.AddUserAsync(user);

            return user;
        }


        private string HashPassword(string password)
        {
            byte[] salt = { 0x1, 0x45, 0x14, 0x98, 0x15, 0x23, 0x90, 0x33, 0x4, 0x6, 0x2, 0x36, 0x78, 0x23 };

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

            string saltedHashedPassword = $"{Convert.ToBase64String(salt)}${hashed}";

            return saltedHashedPassword;
        }

        public async Task<User> LoginAsync(Login model)
        {

            var user = await _userRepository.GetUserByEmailAsync(model.Email);


            if (user != null && VerifyPassword(model.Password, user.Password))
            {
                return user;
            }

            throw new AuthenticationException("Authentication failed. Invalid email or password.");
        }

        public string GenerateJwtToken(
            User user,
            string key, 
            string issuer
            )
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


        private bool VerifyPassword(
            string password,
            string hashedPassword
            )
        {
            if (HashPassword(password) == hashedPassword)
            {
                return true;
            }

            return false;
        }
    }
}
