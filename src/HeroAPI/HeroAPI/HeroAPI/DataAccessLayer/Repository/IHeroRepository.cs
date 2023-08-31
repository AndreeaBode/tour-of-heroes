using System.Collections.Generic;
using HeroAPI.DataAccessLayer.Models;

namespace HeroAPI.DataAccesLayer.Repositories
{
    public interface IHeroRepository
    {
        IEnumerable<Hero> GetAllHeroes();
        Hero GetHeroById(long id);
        void AddHero(Hero hero);
        void UpdateHero(Hero hero);
        void DeleteHero(long id);
    }
}
