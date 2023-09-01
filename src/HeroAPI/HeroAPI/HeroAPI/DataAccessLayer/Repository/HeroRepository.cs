using System;
using Microsoft.EntityFrameworkCore;
using HeroAPI.DataAccessLayer.Models;


namespace HeroAPI.DataAccesLayer.Repositories
{
    public class HeroRepository : IHeroRepository
    {
        private readonly HeroContext _context;

        public HeroRepository(HeroContext context)
        {
            _context = context;
        }

        public IEnumerable<Hero> GetAllHeroes()
        {
            return _context
                .Heroes
                .ToList();
        }

        public Hero? GetHeroById(long id)
        {
            return _context
                .Heroes
                .FirstOrDefault(hero => hero.Id == id);
        }

        public async Task AddHeroAsync(Hero hero)
        {
            _context
                .Heroes
                .Add(hero);

            _context
                .SaveChanges();
        }

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
