using HeroAPI.BusinessLogicLayer.DTOs;
using HeroAPI.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroAPI.DataAccessLayer.Repository
{
    public class PowerRepository : IPowerRepository
    {
        private readonly HeroContext _context;

        public PowerRepository(HeroContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Power>> GetAllPowersAsync()
        {
            return await _context.Powers.ToListAsync();
        }

        public async Task<IEnumerable<Power>> GetPowersByIdsAsync(IEnumerable<int> powerIds)
        {
            return await _context.Powers.Where(p => powerIds.Contains(p.Id)).ToListAsync();
        }

        public async Task<IEnumerable<Power>> GetPowersByIds(IEnumerable<int> powerIds)
        {
            return await _context.Powers.Where(p => powerIds.Contains(p.Id)).ToListAsync();
        }
        public async Task UpdatePowerAsync(Power updatedPower)
        {
            if (updatedPower == null)
            {
                throw new ArgumentNullException(nameof(updatedPower));
            }

            var existingPower = await _context.Powers.FirstOrDefaultAsync(p => p.Id == updatedPower.Id);

            if (existingPower == null)
            {
                throw new ArgumentException("Power not found", nameof(updatedPower));
            }

            existingPower.Name = updatedPower.Name;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePowerAsync(int powerId)
        {
            var powerToRemove = await _context.Powers.FirstOrDefaultAsync(p => p.Id == powerId);

            if (powerToRemove != null)
            {
                _context.Powers.Remove(powerToRemove);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Power> GetPowerByNameAsync(string powerName)
        {
            var power = await _context.Powers.FirstOrDefaultAsync(p => p.Name == powerName);
            return power;
        }
        
        
    }
}
