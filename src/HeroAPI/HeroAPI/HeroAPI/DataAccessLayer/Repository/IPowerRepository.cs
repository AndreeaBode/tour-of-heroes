using HeroAPI.DataAccessLayer.Models;

namespace HeroAPI.DataAccessLayer.Repository
{
    public interface IPowerRepository
    {
        Task<IEnumerable<Power>> GetAllPowersAsync();
        Task<Power> GetPowerByIdAsync(long powerId);
        Task<Power> CreatePowerAsync(Power newPower);
        Task UpdatePowerAsync(long powerId, Power updatedPower); 
        Task DeletePowerAsync(long powerId);
    }
}
