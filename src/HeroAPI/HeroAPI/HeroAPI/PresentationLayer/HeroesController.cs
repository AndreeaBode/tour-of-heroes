using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using HeroAPI.BusinessLogicLayer;
using HeroAPI.DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;

namespace HeroAPI.PresentationLayer
{
    /// <summary>
    /// Controller responsible for handling HTTP requests related to hero entities.
    /// </summary>
    [Route("api/Heroes")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly IHeroService _heroService;

        public HeroesController(IHeroService heroService)
        {
            _heroService = heroService;
        }

        /// <summary>
        /// Retrieves a collection of heroes.
        /// </summary>
        /// <returns>An HTTP response containing the collection of heroes.</returns>
        [HttpGet]
        public IActionResult GetHeroes()
        {
            var heroes = _heroService.GetHeroes();
            return Ok(heroes);
        }

        /// <summary>
        /// Retrieves a specific hero by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the hero to retrieve.</param>
        /// <returns>An HTTP response containing the hero, if found; otherwise, a not found response.</returns>
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

        /// <summary>
        /// Creates a new hero.
        /// </summary>
        /// <param name="hero">The hero entity to create.</param>
        /// <returns>An HTTP response indicating success and the created hero.</returns>
        [Authorize(Policy = "RequireLoggedIn")]
        [HttpPost]
        public IActionResult CreateHero(Hero hero)
        {
            var createdHero = _heroService.CreateHero(hero);
            return CreatedAtAction(nameof(GetHero), new { id = createdHero.Id }, createdHero);
        }

        /// <summary>
        /// Updates an existing hero.
        /// </summary>
        /// <param name="id">The unique identifier of the hero to update.</param>
        /// <param name="hero">The updated hero entity.</param>
        /// <returns>
        /// An HTTP response indicating success and the updated hero, 
        /// or a not found response if the hero does not exist, 
        /// or a bad request response if the provided identifier does not match the hero's ID.
        /// </returns>
        [Authorize(Policy = "RequireLoggedIn")]
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

            return Ok(updatedHero);
        }

        /// <summary>
        /// Deletes a hero by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the hero to delete.</param>
        /// <returns>
        /// An HTTP response indicating success with no content, 
        /// or a not found response if the hero does not exist.
        /// </returns>
        [Authorize(Policy = "RequireLoggedIn")]
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
