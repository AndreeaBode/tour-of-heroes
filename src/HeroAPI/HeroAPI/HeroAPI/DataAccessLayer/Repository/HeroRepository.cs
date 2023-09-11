using System;
using Microsoft.EntityFrameworkCore;
using HeroAPI.DataAccessLayer.Models;


namespace HeroAPI.DataAccesLayer.Repositories
{
    /// <summary>
    /// Repository class responsible for database operations related to Hero entities.
    /// </summary>
    public class HeroRepository : IHeroRepository
    {
        private readonly HeroContext _context;

        public HeroRepository(HeroContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a collection of all heroes from the database.
        /// </summary>
        /// <returns>An enumerable collection of heroes.</returns>
        public IEnumerable<Hero> GetAllHeroes()
        {
            return _context
                .Heroes
                .ToList();
        }

        /// <summary>
        /// Retrieves a specific hero by its unique identifier from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the hero.</param>
        /// <returns>The hero with the specified identifier.</returns>
        public Hero? GetHeroById(long id)
        {
            return _context
                .Heroes
                .FirstOrDefault(hero => hero.Id == id);
        }

        /// <summary>
        /// Adds a new hero to the database asynchronously.
        /// </summary>
        /// <param name="hero">The hero entity to add.</param>
        public async Task AddHeroAsync(Hero hero)
        {
            _context
                .Heroes
                .Add(hero);

            _context
                .SaveChanges();
        }

        /// <summary>
        /// Updates an existing hero in the database asynchronously.
        /// </summary>
        /// <param name="hero">The updated hero entity.</param>
        public async Task UpdateHeroAsync(Hero hero)
        {
            try
            {
                var existingHero = _context.
                    Heroes.
                    Find(hero.Id);

                if (existingHero == null)
                {
                    return;
                }

                existingHero.Name = hero.Name;
                existingHero.Power = hero.Power;
                existingHero.Description = hero.Description;
                existingHero.ImageUrl = hero.ImageUrl;

                _context
                    .SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to update hero.", ex);
            }
        }


        /// <summary>
        /// Deletes a hero from the database asynchronously by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the hero to delete.</param>
        public async Task DeleteHeroAsync(long id)
        {
            var heroToRemove = _context
                .Heroes
                .Find(id);

            if (heroToRemove != null)
            {
                _context
                    .Heroes
                    .Remove(heroToRemove);

                 _context
                    .SaveChanges();
            }
        }

        
    }
}
