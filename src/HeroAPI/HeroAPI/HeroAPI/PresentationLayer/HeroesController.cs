using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using HeroAPI.BusinessLogicLayer;
using HeroAPI.BusinessLogicLayer.DTOs;
using HeroAPI.DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        private readonly IPowerService _powerService;

        public HeroesController(IHeroService heroService, IPowerService powerService)
        {
            _heroService = heroService;
            _powerService = powerService;
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
        public IActionResult GetHero(int id)
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
        public async Task<IActionResult> CreateHeroAsync(HeroDTO heroDTO)
        {

            var hero = new Hero
            {
                Name = heroDTO.Name,
                ImageUrl = heroDTO.ImageUrl,
                Description = heroDTO.Description,
            };
            var createdHero = _heroService.CreateHero(hero);
            var powersArray = heroDTO.Power.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);


            Console.WriteLine(powersArray);
            Console.WriteLine(powersArray.ToString());

            foreach (var powerName in powersArray)
            {
                Console.WriteLine(powerName);
                var power = await _powerService.GetPowerByNameAsync(powerName);
                if (power != null)
                {
                    HeroPower? heroPower = new HeroPower
                    {
                        HeroId = createdHero.Id,
                        PowerId = power.Id,
                    };
                    await _heroService.AddHeroPowerAsync(heroPower);
                }
            }

            heroDTO.Id = createdHero.Id;
            return Ok(heroDTO);
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
        public async Task<IActionResult> UpdateHeroAsync(int id, HeroDTO updatedHeroDTO)
        {
            HeroDTO? existingHero = _heroService.GetHero(id);
            if (existingHero == null)
            {
                return NotFound();
            }

            existingHero.Name = updatedHeroDTO.Name;
            existingHero.ImageUrl = updatedHeroDTO.ImageUrl;
            existingHero.Description = updatedHeroDTO.Description;

       
            var powersArray = updatedHeroDTO.Power.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var updatedPowers = new List<Power>();


            
            var existingPowers = await _heroService.GetHeroPowersAsync(existingHero.Id); ;
            var ePowers = existingPowers.Select(hp => hp.Power.Name).ToList();

            var toRemovePowers = ePowers.Except(powersArray).ToList();

            foreach (var remainingPower in toRemovePowers)
            {
                var heroPow = existingPowers.Where(hp => hp.Power.Name == remainingPower).First();
                await _heroService.RemoveHeroPowerAsync(existingHero.Id, heroPow.Id);
            }

            var newPowers = powersArray.Except(ePowers).ToList();
            foreach (var powerName in newPowers)
            {
                var power = await _powerService.GetPowerByNameAsync(powerName);

                if (power == null)
                {
                    continue;
                }

                await _heroService.AddHeroPowerAsync(new HeroPower
                {
                    HeroId = existingHero.Id,
                    PowerId = power.Id
                });
            }

            var hero = new Hero
            {
                Id = existingHero.Id,
                Name = existingHero.Name,
                Description = existingHero.Description,
                ImageUrl = existingHero.ImageUrl,
            };

            await _heroService.UpdateHero(hero);

            return Ok(existingHero);
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
        public IActionResult DeleteHero(int id)
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