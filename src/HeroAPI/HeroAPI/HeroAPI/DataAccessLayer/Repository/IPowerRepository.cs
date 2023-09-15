using HeroAPI.DataAccessLayer.Models;

namespace HeroAPI.DataAccessLayer.Repository
{
    public interface IPowerRepository
    {
        Task<IEnumerable<Power>> GetAllPowersAsync();
        Task<IEnumerable<Power>> GetPowersByIdsAsync(IEnumerable<int> powerIds);
        Task<IEnumerable<Power>> GetPowersByIds(IEnumerable<int> powerIds);
        Task UpdatePowerAsync(Power updatedPower);
        Task DeletePowerAsync(int powerId);

        Task<Power> GetPowerByNameAsync(string powerName);
       
    }
}
