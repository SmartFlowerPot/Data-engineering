using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Persistence;
using WebAPI.Persistence.Interface;
using WebAPI.Services.Interface;

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

        public async Task<Plant> GetPlantByDeviceAsync(string eui)
        {
            var noAge = await _plantRepo.GetPlantByDeviceAsync(eui);
            noAge.SetAge();
            return noAge;
        }

        public async Task<Plant> DeletePlantAsync(string eui)
        {
            return await _plantRepo.DeletePlantAsync(eui);
        }
    }
}