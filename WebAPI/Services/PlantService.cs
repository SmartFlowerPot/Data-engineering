using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Persistence;

namespace WebAPI.Services
{
    public class PlantService : IPlantService
    {
        private readonly IPlantRepo _plantRepo;

        public PlantService(IPlantRepo plantRepo)
        {
            this._plantRepo = plantRepo;
        }

        public Task<Plant> PostPlantAsync(Plant plant, string username)
        {
            return _plantRepo.PostPlantAsync(plant, username);
        }

        public Task<Plant> GetPlantByDeviceAsync(string eui)
        {
            return _plantRepo.GetPlantByDeviceAsync(eui);
        }

        public async Task<Plant> DeletePlantAsync(string eui)
        {
            return await _plantRepo.DeletePlantAsync(eui);
        }
    }
}