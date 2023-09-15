using System.Collections.Generic;
using HeroAPI.DataAccessLayer.Models;
using HeroAPI.DataAccesLayer.Repositories;
using HeroAPI.BusinessLogicLayer.DTOs;
using HeroAPI.DataAccessLayer.Repository;

namespace HeroAPI.BusinessLogicLayer
{
    /// <summary>
    /// Service class responsible for handling operations related to Hero entities.
    /// </summary>
    public class HeroService : IHeroService
    {
        private readonly IHeroRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IPowerRepository _powerRepository;

        public HeroService(IHeroRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves a collection of all heroes.
        /// </summary>
        public IEnumerable<HeroDTO> GetHeroes()
        {
            var heroes = _repository.GetAllHeroes();

            var resultHero = new List<HeroDTO>();
            foreach (var hero in heroes)
            {
                var heroDTO = new HeroDTO
                {
                    Id = hero.Id,
                    Name = hero.Name,
                    ImageUrl = hero.ImageUrl,
                    Description = hero.Description,
                    Power = string.Join(", ", hero.HeroPowers.Select(heroPower => heroPower.Power.Name)),
                };

                resultHero.Add(heroDTO);
            }
            
            return resultHero;
        }

        /// <summary>
        /// Retrieves a specific hero by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the hero.</param>
        /// <returns>The hero with the specified identifier.</returns>
        public HeroDTO GetHero(int id)
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
        public async Task<Hero> UpdateHero(Hero hero)
        {
            await _repository.UpdateHeroAsync(hero);
            return hero;
        }

        /// <summary>
        /// Deletes a hero entity from the repository by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the hero to delete.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        public bool DeleteHero(int id)
        {
            _repository.DeleteHeroAsync(id);
            return true;
        }

        public async Task AddHeroPowerAsync(HeroPower heroPower)
        {

            await _repository.AddHeroPowerAsync(heroPower);
        }

        public async Task<List<HeroPower>> GetHeroPowersAsync(int id)
        {
            return await _repository.GetHeroPowersAsync(id);
        }

        public async Task RemoveHeroPowerAsync(int heroId, int powerId)
        {
            await _repository.RemoveHeroPowerAsync(heroId, powerId);
        }

    }
}
