using HeroAPI.DataAccessLayer.Models;

namespace HeroAPI.DataAccessLayer.Repository
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
    }
}
