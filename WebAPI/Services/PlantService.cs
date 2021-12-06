﻿using System.Collections.Generic;
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
    }
}