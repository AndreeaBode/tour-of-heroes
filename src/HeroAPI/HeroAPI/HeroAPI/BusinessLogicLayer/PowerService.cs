using HeroAPI.DataAccessLayer.Models;
using HeroAPI.DataAccessLayer.Repository;

namespace HeroAPI.BusinessLogicLayer
{
    public class PowerService : IPowerService
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

        public async Task<Power> GetPowerByIdAsync(long powerId)
        {
            return await _powerRepository.GetPowerByIdAsync(powerId);
        }

        public async Task<Power> CreatePowerAsync(Power newPower)
        {
            if (newPower == null)
            {
                throw new ArgumentNullException(nameof(newPower));
            }

            return await _powerRepository.CreatePowerAsync(newPower);
        }

        public async Task UpdatePowerAsync(long powerId, Power updatedPower)
        {
            if (updatedPower == null)
            {
                throw new ArgumentNullException(nameof(updatedPower));
            }

            await _powerRepository.UpdatePowerAsync(powerId, updatedPower);
        }

        public async Task DeletePowerAsync(long powerId)
        {
            await _powerRepository.DeletePowerAsync(powerId);
        }
    }
}
