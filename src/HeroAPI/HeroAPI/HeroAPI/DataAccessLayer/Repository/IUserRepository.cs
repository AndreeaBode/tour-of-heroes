using HeroAPI.DataAccessLayer.Models;

namespace HeroAPI.DataAccessLayer.Repository
{
     /// <summary>
     /// Interface defining the contract for database operations related to User entities.
     /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Adds a new user to the database asynchronously.
        /// </summary>
        /// <param name="user">The user entity to add.</param>
        /// <returns>The added user.</returns>
        Task<User> AddUserAsync(User user);

        /// <summary>
        /// Retrieves a user by their email address from the database asynchronously.
        /// </summary>
        /// <param name="email">The email address of the user to retrieve.</param>
        /// <returns>The user with the specified email address.</returns>
        Task<User> GetUserByEmailAsync(string email);
    }
}
