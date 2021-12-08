﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Persistence
{
    public interface IPlantRepo
    {
        Task<Plant> PostPlantAsync(Plant plant, string username);
        Task<Plant> GetPlantByDeviceAsync(string eui);
        Task<Plant> DeletePlantAsync(string eui);
    }
}