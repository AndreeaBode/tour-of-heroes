using System.Collections.Generic;
using HeroAPI.DataAccessLayer.Models;

namespace HeroAPI.DataAccesLayer.Repositories
{
    public interface IHeroRepository
    {
        IEnumerable<Hero> GetAllHeroes();
        Hero GetHeroById(long id);
        Task AddHeroAsync(Hero hero);
        Task UpdateHeroAsync(Hero hero);
        Task DeleteHeroAsync(long id);
    }
}
