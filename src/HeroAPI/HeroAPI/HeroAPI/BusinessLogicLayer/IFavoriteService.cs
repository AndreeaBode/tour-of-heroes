using HeroAPI.BusinessLogicLayer.DTOs;
using HeroAPI.DataAccessLayer.Models;

namespace HeroAPI.BusinessLogicLayer
{
    public interface IFavoriteService
    {
        Task<IEnumerable<HeroDTO>> GetFavoriteHeroesAsync(int userId);
        Task<HeroUser> AddFavoriteHeroesAsync(int userId, int heroId);
    }
}
