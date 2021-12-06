using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IPlantService
    {
        Task<Plant> PostPlantAsync(Plant plant, string username);
    }
}