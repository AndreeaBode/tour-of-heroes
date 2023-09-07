using HeroAPI.BusinessLogicLayer;
using HeroAPI.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace HeroAPI.PresentationLayer
{
    [ApiController]
    [Route("api/powers")]
    public class PowerController : ControllerBase
    {
        private readonly IPowerService _powerService;

        public PowerController(IPowerService powerService)
        {
            _powerService = powerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Power>>> GetAllPowersAsync()
        {
            var powers = await _powerService.GetAllPowersAsync();
            return Ok(powers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Power>> GetPowerByIdAsync(long id)
        {
            var power = await _powerService.GetPowerByIdAsync(id);

            if (power == null)
            {
                return NotFound();
            }

            return Ok(power);
        }

        [HttpPost]
        public async Task<ActionResult<Power>> CreatePowerAsync(Power newPower)
        {
            if (newPower == null)
            {
                return BadRequest();
            }

            var createdPower = await _powerService.CreatePowerAsync(newPower);
            return CreatedAtAction(nameof(GetPowerByIdAsync), new { id = createdPower.Id }, createdPower);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePowerAsync(long id, Power updatedPower)
        {
            if (id != updatedPower.Id)
            {
                return BadRequest();
            }

            try
            {
                await _powerService.UpdatePowerAsync(id, updatedPower); 
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePowerAsync(long id)
        {
            try
            {
                await _powerService.DeletePowerAsync(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
