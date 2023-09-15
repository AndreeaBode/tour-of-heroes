using System;
using Microsoft.EntityFrameworkCore;
using HeroAPI.DataAccessLayer.Models;
using HeroAPI.BusinessLogicLayer.DTOs;
using HeroAPI.Migrations;
using System.Linq;

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
                .Include(hero => hero.HeroPowers)
                .ThenInclude(heroPower => heroPower.Power)
                .ToList();


        }

        /// <summary>
        /// Retrieves a specific hero by its unique identifier from the database.
        /// </summary>
        /// <param name="id">The unique identifier of the hero.</param>
        /// <returns>The hero with the specified identifier.</returns>
        public HeroDTO? GetHeroById(int id)
        {
            var hero = _context
                .Heroes
                .AsNoTracking()
                .Include(hero => hero.HeroPowers)
                .ThenInclude(heroPower => heroPower.Power)
                .FirstOrDefault(hero => hero.Id == id);


            var heroDTO = new HeroDTO
            {
                Id = hero.Id,
                Name = hero.Name,
                ImageUrl = hero.ImageUrl,
                Description = hero.Description,
                Power = string.Join(", ", hero.HeroPowers.Select(heroPower => heroPower.Power.Name)),
            };

            return heroDTO;
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
        public async Task UpdateHeroAsync(HeroDTO heroDTO)
        {
            try
            {
                var existingHero = _context
                .Heroes
                .Include(hero => hero.HeroPowers)
                .ThenInclude(heroPower => heroPower.Power)
                .FirstOrDefault(h => h.Id == heroDTO.Id);

                if (existingHero == null)
                {
                    return;
                }

                existingHero.Name = heroDTO.Name;
                existingHero.ImageUrl = heroDTO.ImageUrl;
                existingHero.Description = heroDTO.Description;
                
                _context
                    .SaveChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to update hero.", ex);
            }
        }
        public async Task UpdateHeroAsync(Hero hero)
        {
            _context.Update(hero);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a hero from the database asynchronously by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the hero to delete.</param>
        public async Task DeleteHeroAsync(int id)
        {
            var heroToRemove = _context
                .Heroes
                .FirstOrDefault(h => h.Id == id);
            Console.WriteLine(heroToRemove);

            if (heroToRemove != null)
            {
                _context
                    .Heroes
                    .Remove(heroToRemove);

                _context
                   .SaveChanges();
            }
        }
        public async Task AddHeroPowerAsync(HeroPower heroPower)
        {
            _context.HeroPowers.Add(heroPower);
          await  _context.SaveChangesAsync();
        }
        public async Task<List<HeroPower>> GetHeroPowersAsync(int heroId)
        {
            return await _context.HeroPowers
                .Where(hp => hp.HeroId == heroId)
                .Include(hp => hp.Power) 
                .ToListAsync();
        }

        public async Task RemoveHeroPowerAsync(int heroId, int powerId)
        {
            var heroPowerToRemove = await _context.HeroPowers
                .FirstOrDefaultAsync(hp => hp.HeroId == heroId && hp.PowerId == powerId);

            if (heroPowerToRemove != null)
            {
                _context.HeroPowers.Remove(heroPowerToRemove);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Hero>> GetAllHeroesByUserIdAsync(int userId)
        {
            /*var existingHero = _context
                .Heroes
                .Include(hero => hero.HeroPowers)
                .ThenInclude(heroPower => heroPower.Power)
                .FirstOrDefault(h => h.Id == heroDTO.Id);
*/

            var heroIds = _context
                .HeroUsers
                .Where(user => user.UserId == userId)
                .Select(user => user.HeroId)
                .ToList();

            var hero = _context
                .Heroes
                .Where(h => heroIds.Contains(h.Id))
                .ToList();

            return hero;
        }   
    }
}
