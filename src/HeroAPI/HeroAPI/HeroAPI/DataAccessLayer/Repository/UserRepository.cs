using Microsoft.EntityFrameworkCore;
using HeroAPI.DataAccessLayer.Models;
using HeroAPI.DataAccessLayer.Repository;

namespace HeroAPI.DataAccesLayer.Repositories
{
    /// <summary>
    /// Repository class responsible for database operations related to User entities.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly HeroContext _context;

        public UserRepository(HeroContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new user to the database asynchronously.
        /// </summary>
        /// <param name="user">The user entity to add.</param>
        /// <returns>The added user.</returns>
        public async Task<User> AddUserAsync(User user)
        {
            _context
                .Users
                .Add(user);  
            await _context
                .SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// Retrieves a user by their email address from the database asynchronously.
        /// </summary>
        /// <param name="email">The email address of the user to retrieve.</param>
        /// <returns>The user with the specified email address, if found; otherwise, null.</returns>
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.
                Users.
                FirstOrDefaultAsync(x => x.Email == email);
        }
       /* public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.HeroUsers.FindAsync(userId);
        }*/
    }
}
