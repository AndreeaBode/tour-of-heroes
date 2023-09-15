using HeroAPI.BusinessLogicLayer.DTOs;
using HeroAPI.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroAPI.DataAccessLayer.Repository
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly HeroContext _context;

        public FavoriteRepository(HeroContext context)
        {
            _context = context;
        }

        public async Task<HeroUser> AddHeroesToUserAsync(int userId, int heroId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return null;
            }
            var hero = await _context.Heroes.FindAsync(heroId);
            if (hero == null)
            {
                return null;
            }
            var heroUser = new HeroUser
            {
                UserId = userId,
                HeroId = heroId
            };
            _context.HeroUsers.Add(heroUser);
            await _context.SaveChangesAsync();

            return heroUser;
        }
    }
}
