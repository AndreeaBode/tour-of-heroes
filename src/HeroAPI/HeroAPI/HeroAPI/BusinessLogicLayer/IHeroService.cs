using System.Collections.Generic;
using HeroAPI.DataAccessLayer.Models;
using HeroAPI.BusinessLogicLayer.DTOs;

namespace HeroAPI.BusinessLogicLayer
{
    /// <summary>
    /// Represents a service interface for managing Hero entities.
    /// </summary>
    public interface IHeroService
    {
        /// <summary>
        /// Retrieves a collection of all heroes.
        /// </summary>
        /// <returns>An enumerable collection of heroes.</returns>
        IEnumerable<HeroDTO> GetHeroes();

        /// <summary>
        /// Retrieves a specific hero by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the hero.</param>
        /// <returns>The hero with the specified identifier.</returns>
        HeroDTO GetHero(int id);

        /// <summary>
        /// Creates a new hero entity.
        /// </summary>
        /// <param name="hero">The hero entity to create.</param>
        /// <returns>The created hero entity.</returns>
        Hero CreateHero(Hero hero);

        /// <summary>
        /// Updates an existing hero entity.
        /// </summary>
        /// <param name="hero">The hero entity to update.</param>
        /// <returns>The updated hero entity.</returns>
        Task<Hero> UpdateHero(Hero hero);

        /// <summary>
        /// Deletes a hero entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the hero to delete.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        bool DeleteHero(int id);
        Task AddHeroPowerAsync(HeroPower heroPower);

        Task<List<HeroPower>> GetHeroPowersAsync(int id);
        Task RemoveHeroPowerAsync(int heroId, int powerId);

        
    }
}
