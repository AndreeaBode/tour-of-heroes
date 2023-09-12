using System.Collections.Generic;
using HeroAPI.DataAccessLayer.Models;
using HeroAPI.DataAccesLayer.Repositories;

namespace HeroAPI.BusinessLogicLayer
{
/// <summary>
 /// Service class responsible for handling operations related to Hero entities.
 /// </summary>
    public class HeroService : IHeroService
    {
        private readonly IHeroRepository _repository;

        public HeroService(IHeroRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves a collection of all heroes.
        /// </summary>
        public IEnumerable<Hero> GetHeroes()
        {
            return _repository.GetAllHeroes();
        }

        /// <summary>
        /// Retrieves a specific hero by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the hero.</param>
        /// <returns>The hero with the specified identifier.</returns>
        public Hero GetHero(int id)
        {
            return _repository.GetHeroById(id);
        }

        /// <summary>
        /// Creates a new hero entity and adds it to the repository.
        /// </summary>
        /// <param name="hero">The hero entity to create and add.</param>
        /// <returns>The created hero entity.</returns>
        public Hero CreateHero(Hero hero)
        {
            _repository.AddHeroAsync(hero);
            return hero;
        }

        /// <summary>
        /// Updates an existing hero entity in the repository.
        /// </summary>
        /// <param name="hero">The hero entity to update.</param>
        /// <returns>The updated hero entity.</returns>
        public Hero UpdateHero(Hero hero)
        {
            _repository.UpdateHeroAsync(hero);
            return hero;
        }

        /// <summary>
        /// Deletes a hero entity from the repository by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the hero to delete.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        public bool DeleteHero(long id)
        {
            _repository.DeleteHeroAsync(id);
            return true;
        }
    }
}
