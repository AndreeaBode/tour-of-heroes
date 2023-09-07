using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HeroAPI.DataAccessLayer.Models;

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

        public async Task<Power> GetPowerByIdAsync(long powerId)
        {
            return await _context.Powers.FirstOrDefaultAsync(p => p.Id == powerId);
        }

        public async Task<Power> CreatePowerAsync(Power newPower)
        {
            if (newPower == null)
            {
                throw new ArgumentNullException(nameof(newPower));
            }

            _context.Powers.Add(newPower);
            await _context.SaveChangesAsync();

            return newPower;
        }

        public async Task UpdatePowerAsync(long powerId, Power updatedPower)
        {
            if (updatedPower == null)
            {
                throw new ArgumentNullException(nameof(updatedPower));
            }

            var existingPower = await _context.Powers.FirstOrDefaultAsync(p => p.Id == powerId);

            if (existingPower == null)
            {
                throw new ArgumentException("Power not found", nameof(powerId));
            }

            existingPower.Name = updatedPower.Name;
            existingPower.Description = updatedPower.Description;

            await _context.SaveChangesAsync();
        }

        public async Task DeletePowerAsync(long powerId)
        {
            var powerToRemove = await _context.Powers.FirstOrDefaultAsync(p => p.Id == powerId);

            if (powerToRemove != null)
            {
                _context.Powers.Remove(powerToRemove);
                await _context.SaveChangesAsync();
            }
        }
    }
}
