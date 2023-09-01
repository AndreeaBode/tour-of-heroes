using HeroAPI.DataAccessLayer.Models;

namespace HeroAPI.BusinessLogicLayer
{
    public interface IUserService
    {
        Task<User?> RegisterAsync(Register model);
        Task<User> LoginAsync(Login model);
        string GenerateJwtToken(
            User user, 
            string key, 
            string issuer
            );
    }
}
