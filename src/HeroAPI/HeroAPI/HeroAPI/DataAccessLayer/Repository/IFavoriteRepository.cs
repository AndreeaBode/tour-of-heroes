using HeroAPI.BusinessLogicLayer.DTOs;
using HeroAPI.DataAccessLayer.Models;

namespace HeroAPI.DataAccessLayer.Repository
{
    public interface IFavoriteRepository
    {
        Task<HeroUser> AddHeroesToUserAsync(int userId, int heroId);
    }
}
