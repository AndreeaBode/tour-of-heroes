using System.Collections.Generic;
using HeroAPI.DataAccessLayer.Models;

namespace HeroAPI.DataAccesLayer.Repositories
{
    /// <summary>
    /// Interface defining the contract for database operations related to Hero entities.
    /// </summary>
    public interface IHeroRepository
    {
        /// <summary>
        /// Retrieves a collection of all heroes from the database.
        /// </summary>
        /// <returns>An enumerable collection of heroes.</returns>
        IEnumerable<Hero> GetAllHeroes();

        /// <summary>
        /// Retrieves a specific hero by its unique identifier from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the hero.</param>
        /// <returns>The hero with the specified identifier.</returns>
        Hero GetHeroById(long id);

        /// <summary>
        /// Adds a new hero to the database asynchronously.
        /// </summary>
        /// <param name="hero">The hero entity to add.</param>
        Task AddHeroAsync(Hero hero);

        /// <summary>
        /// Updates an existing hero in the database asynchronously.
        /// </summary>
        /// <param name="hero">The updated hero entity.</param>
        Task UpdateHeroAsync(Hero hero);

        /// <summary>
        /// Deletes a hero from the database asynchronously by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the hero to delete.</param>
        Task DeleteHeroAsync(long id);
    }
}
