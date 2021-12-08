using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IPlantService
    {
        Task<Plant> PostPlantAsync(Plant plant, string username);
        Task<Plant> GetPlantByDeviceAsync(string eui);
        Task<Plant> DeletePlantAsync(string eui);
    }
}