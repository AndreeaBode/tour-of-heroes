using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using HeroAPI.BusinessLogicLayer;
using HeroAPI.DataAccessLayer.Models;

namespace HeroAPI.PresentationLayer
{
    [Route("api/Heroes")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly IHeroService _heroService;

        public HeroesController(IHeroService heroService)
        {
            _heroService = heroService;
        }

        [HttpGet]
        public IActionResult GetHeroes()
        {
            var heroes = _heroService.GetHeroes();
            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public IActionResult GetHero(long id)
        {
            var hero = _heroService.GetHero(id);
            if (hero == null)
            {
                return NotFound();
            }
            return Ok(hero);
        }

        [HttpPost]
        public IActionResult CreateHero(Hero hero)
        {
            var createdHero = _heroService.CreateHero(hero);
            return CreatedAtAction(nameof(GetHero), new { id = createdHero.Id }, createdHero);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateHero(long id, Hero hero)
        {
            if (id != hero.Id)
            {
                return BadRequest();
            }

            var updatedHero = _heroService.UpdateHero(hero);
            if (updatedHero == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHero(long id)
        {
            var result = _heroService.DeleteHero(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
