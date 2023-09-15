using System.Collections.Generic;
using HeroAPI.DataAccessLayer.Models;
using HeroAPI.DataAccesLayer.Repositories;
using HeroAPI.BusinessLogicLayer.DTOs;
using HeroAPI.DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HeroAPI.BusinessLogicLayer
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IHeroRepository _heroRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IUserRepository _userRepository;
        public FavoriteService(IHeroRepository heroRepository, IFavoriteRepository favoriteRepository, IUserRepository userRepository)
        {
            _heroRepository = heroRepository;
            _favoriteRepository = favoriteRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<HeroDTO>> GetFavoriteHeroesAsync(int userId)
        {

            var heroes = await _heroRepository.GetAllHeroesByUserIdAsync(userId);
            var result = new List<HeroDTO>();
            foreach (var hero in heroes)
            {
                var newHero = new HeroDTO
                {
                    Id = hero.Id,
                    Name = hero.Name,
                    Description = hero.Description,
                    ImageUrl = hero.ImageUrl,
                };
                result.Add(newHero);
            }
            return result;
        }

        public async Task<HeroUser> AddFavoriteHeroesAsync(int userId, int heroId)
        {

            var heroes = await _favoriteRepository.AddHeroesToUserAsync(userId, heroId);

            return heroes;
        }
    }        
}
