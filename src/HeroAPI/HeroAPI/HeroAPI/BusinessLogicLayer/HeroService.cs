using System.Collections.Generic;
using HeroAPI.DataAccessLayer.Models;
using HeroAPI.DataAccesLayer.Repositories;

namespace HeroAPI.BusinessLogicLayer
{
    public class HeroService : IHeroService
    {
        private readonly IHeroRepository _repository;

        public HeroService(IHeroRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Hero> GetHeroes()
        {
            return _repository.GetAllHeroes();
        }

        public Hero GetHero(long id)
        {
            return _repository.GetHeroById(id);
        }

        public Hero CreateHero(Hero hero)
        {
            _repository.AddHeroAsync(hero);
            return hero;
        }

        public Hero UpdateHero(Hero hero)
        {
            _repository.UpdateHeroAsync(hero);
            return hero;
        }

        public bool DeleteHero(long id)
        {
            _repository.DeleteHeroAsync(id);
            return true;
        }
    }
}
