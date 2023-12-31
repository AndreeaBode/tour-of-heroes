﻿using HeroAPI.DataAccessLayer.Models;
using System.Threading.Tasks;

namespace HeroAPI.BusinessLogicLayer
{
    public interface IPowerService
    {
        Task<IEnumerable<Power>> GetAllPowersAsync();
        Task UpdatePowerAsync(Power updatedPower);
        Task DeletePowerAsync(int powerId);
        Task<Power> GetPowerByNameAsync(string powerName);
        
    }
}
