using HeroAPI.DataAccessLayer.Models;
using HeroAPI.DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace HeroAPI.BusinessLogicLayer
{
    public class PowerService: IPowerService
    {
        private readonly IPowerRepository _powerRepository;

        public PowerService(IPowerRepository powerRepository)
        {
            _powerRepository = powerRepository;
        }

        public async Task<IEnumerable<Power>> GetAllPowersAsync()
        {
            return await _powerRepository.GetAllPowersAsync();
        }

        public async Task UpdatePowerAsync( Power updatedPower)
        {
            if (updatedPower == null)
            {
                throw new ArgumentNullException(nameof(updatedPower));
            }

            await _powerRepository.UpdatePowerAsync(updatedPower);
        }

        public async Task DeletePowerAsync(int powerId)
        {
            await _powerRepository.DeletePowerAsync(powerId);
        }
        public async Task<Power> GetPowerByNameAsync(string powerName)
        {
            return await _powerRepository.GetPowerByNameAsync(powerName);
        }
        
    }
}
