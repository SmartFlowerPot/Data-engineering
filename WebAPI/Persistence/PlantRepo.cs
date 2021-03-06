using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Persistence.Interface;

namespace WebAPI.Persistence
{
    public class PlantRepo : IPlantRepo
    {
        public async Task<Plant> PostPlantAsync(Plant plant, string username)
        {
            await using var database = new Database();
            
            await database.Plants.AddAsync(plant);

            var accountToAddPlant =
                await database
                    .Accounts
                    .Include(a => a.Plants)
                    .FirstOrDefaultAsync(a => a.Username.Equals(username));
            if (accountToAddPlant == null)
            {
                throw new Exception(Status.UserNotFound);
            }

            accountToAddPlant.Plants.Add(plant);
            
            database.Update(accountToAddPlant);
            await database.SaveChangesAsync();

            return plant;
        }

        public async Task<Plant> GetPlantByDeviceAsync(string eui)
        {
            await using var database = new Database();
            
            var first = await database
                .Plants
                .FirstOrDefaultAsync(p => p.EUI.Equals(eui));
            if (first == null)
            { 
                throw new Exception(Status.DeviceNotFound);
            }
            return first;
        }

        public async Task<Plant> DeletePlantAsync(string eui)
        {
            await using var database = new Database();

            var plant = database
                .Plants.Include(p => p.Measurements)
                .FirstOrDefault(p => p.EUI.Equals(eui));
            if (plant == null)
            {
                throw new Exception(Status.DeviceNotFound);
            }
            database.Measurements.RemoveRange(plant.Measurements);
            database.Plants.Remove(plant);
            await database.SaveChangesAsync();
            return plant;
        }
    }
}