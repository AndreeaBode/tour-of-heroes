using HeroAPI.BusinessLogicLayer.DTOs;
using HeroAPI.DataAccessLayer.Models;

namespace HeroAPI.BusinessLogicLayer
{
    /// <summary>
    /// Represents a service interface for user-related operations such as registration and login.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Registers a new user asynchronously.
        /// </summary>
        /// <param name="model">The registration data.</param>
        /// <returns>The registered user if successful; otherwise, null.</returns>
        Task<User?> RegisterAsync(RegisterDTO model);

        /// <summary>
        /// Performs user login asynchronously.
        /// </summary>
        /// <param name="model">The login credentials.</param>
        /// <returns>The authenticated user.</returns>
        Task<User> LoginAsync(LoginDTO model);

        /// <summary>
        /// Generates a JSON Web Token (JWT) for a given user.
        /// </summary>
        /// <param name="user">The user for whom the token is generated.</param>
        /// <param name="key">The secret key for JWT generation.</param>
        /// <param name="issuer">The issuer of the JWT.</param>
        /// <returns>The generated JWT token.</returns>
        string GenerateJwtToken(
            User user,
            string key,
            string issuer
            );
    }
}