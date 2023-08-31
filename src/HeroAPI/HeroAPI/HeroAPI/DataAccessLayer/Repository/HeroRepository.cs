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
            return _context.Heroes.ToList();
        }

        public Hero GetHeroById(long id)
        {
            return _context.Heroes.FirstOrDefault(hero => hero.Id == id);
        }

        public void AddHero(Hero hero)
        {
            _context.Heroes.Add(hero);
            _context.SaveChanges();
        }

        public void UpdateHero(Hero hero)
        {
            using var transaction = _context.Database.BeginTransaction(); 
            try
            {
                var existingHero = _context.Heroes.Find(hero.Id);
                if (existingHero != null)
                {
                    existingHero.Name = hero.Name;
                    existingHero.Power = hero.Power;
                    existingHero.Description = hero.Description;
                    existingHero.ImageUrl = hero.ImageUrl;
                    _context.SaveChanges();
                }

                transaction.Commit(); 
            }
            catch
            {
                transaction.Rollback(); 
                throw;
            }
        }


        public void DeleteHero(long id)
        {
            var heroToRemove = _context.Heroes.Find(id);
            if (heroToRemove != null)
            {
                _context.Heroes.Remove(heroToRemove);
                _context.SaveChanges();
            }
        }
    }
}
