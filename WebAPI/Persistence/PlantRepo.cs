using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess;
using WebAPI.Exceptions;
using WebAPI.Models;

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
                    .FirstAsync(a => a.Username.Equals(username));
            
            accountToAddPlant.Plants.Add(plant);
            
            database.Update(accountToAddPlant);
            await database.SaveChangesAsync();

            return plant;
        }


    }
}