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
using HeroAPI.BusinessLogicLayer.DTOs;
using NuGet.Common;

namespace HeroAPI.BusinessLogicLayer
{
    /// <summary>
    /// Service class responsible for user-related operations such as registration, login, and JWT token generation.
    /// </summary>
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

        /// <summary>
        /// Registers a new user with the provided registration data.
        /// </summary>
        /// <param name="model">The registration data.</param>
        /// <returns>The registered user, or null if registration fails.</returns>
        public async Task<User?> RegisterAsync(RegisterDTO model)
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

            string[] emailParts = model.Email.Split('@');
            string name = emailParts[0];

            var user = new User
            {
                Name = name,
                Email = model.Email,
                Password = HashPassword(model.Password),
                HeroId = 24,
                Role = model.Role
            };

            await _userRepository.AddUserAsync(user);

            return user;
        }

        /// <summary>
        /// Hashes a password using a secure algorithm.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The salted and hashed password.</returns>
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

        /// <summary>
        /// Authenticates a user with the provided login data.
        /// </summary>
        /// <param name="model">The login data.</param>
        /// <returns>The authenticated user, or null if authentication fails.</returns>
        public async Task<User> LoginAsync(LoginDTO model)
        {

            var user = await _userRepository.GetUserByEmailAsync(model.Email);


            if (user != null && VerifyPassword(model.Password, user.Password))
            {
                return user;
            }

            throw new AuthenticationException("Authentication failed. Invalid email or password.");
        }

        /// <summary>
        /// Generates a JWT token for a user with the specified claims.
        /// </summary>
        /// <param name="user">The user for whom to generate the token.</param>
        /// <param name="key">The JWT signing key.</param>
        /// <param name="issuer">The token issuer.</param>
        /// <returns>The generated JWT token.</returns>
        public string GenerateJwtToken(
                User user,
                string key,
                string issuer,
                string audience)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("Mail", user.Email)
            };

            var keyBytes = Encoding.UTF8.GetBytes(key);
            var keyCredentials = new SymmetricSecurityKey(keyBytes);
            var signInCredentials = new SigningCredentials(keyCredentials, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience, // Set the audience here
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: signInCredentials
            );

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var token = tokenHandler.CreateToken(tokenDescriptor);

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenAsString;
        }


        /// <summary>
        /// Verifies a password against a hashed password.
        /// </summary>
        /// <param name="password">The password to verify.</param>
        /// <param name="hashedPassword">The salted and hashed password to compare against.</param>
        /// <returns>True if the password is verified, otherwise false.</returns>
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
