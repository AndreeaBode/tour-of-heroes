using global::HeroAPI.DataAccessLayer.Models;
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


            [HttpPut("{id}")]
            public async Task<IActionResult> UpdatePowerAsync(long id, Power updatedPower)
            {
                if (id != updatedPower.Id)
                {
                    return BadRequest();
                }

                try
                {
                    await _powerService.UpdatePowerAsync(updatedPower);
                }
                catch (ArgumentException)
                {
                    return NotFound();
                }

                return NoContent();
            }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePowerAsync(int id)
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
