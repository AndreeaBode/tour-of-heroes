using Microsoft.EntityFrameworkCore;
using HeroAPI.DataAccessLayer.Models;
using HeroAPI.DataAccessLayer.Repository;

namespace HeroAPI.DataAccesLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HeroContext _context;

        public UserRepository(HeroContext context)
        {
            _context = context;
        }

        public async Task<User> AddUserAsync(User user)
        {
            _context
                .Users
                .Add(user);  
            await _context
                .SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.
                Users.
                FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
