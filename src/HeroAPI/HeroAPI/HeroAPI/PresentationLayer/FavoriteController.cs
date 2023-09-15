using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using HeroAPI.BusinessLogicLayer;
using HeroAPI.BusinessLogicLayer.DTOs;
using HeroAPI.DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HeroAPI.PresentationLayer
{
    [Route("api/Favorites")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _heroUserService;
        public FavoriteController(IFavoriteService heroUserService)
        {
            _heroUserService = heroUserService;
        }

        [Authorize(Policy = "RequireLoggedIn")]
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<HeroDTO>>> GetFavoriteHeroes(int userId)
        {
            var favoriteHeroes = await _heroUserService.GetFavoriteHeroesAsync(userId);
            return Ok(favoriteHeroes);
        }
        [Authorize(Policy = "RequireLoggedIn")]
        [HttpPost("{userId}")]
        public async Task<IActionResult> AddFavoriteHeroes(int userId, int heroId)
        {
            var favoriteHeroes = await _heroUserService.AddFavoriteHeroesAsync(userId, heroId);
            return Ok(favoriteHeroes);
        }
    }
}
