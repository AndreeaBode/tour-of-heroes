using HeroAPI.DataAccessLayer.Models;

namespace HeroAPI.DataAccessLayer.Repository
{
    public interface IPowerRepository
    {
        Task<IEnumerable<Power>> GetAllPowersAsync();
        Task UpdatePowerAsync(Power updatedPower);
        Task DeletePowerAsync(long powerId);
    }
}
