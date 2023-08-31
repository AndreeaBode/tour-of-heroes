using System.Collections.Generic;
using HeroAPI.DataAccessLayer.Models;

namespace HeroAPI.BusinessLogicLayer
{
    public interface IHeroService
    {
        IEnumerable<Hero> GetHeroes();
        Hero GetHero(long id);
        Hero CreateHero(Hero hero);
        Hero UpdateHero(Hero hero);
        bool DeleteHero(long id);
    }
}
