using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess;
using WebAPI.Models;

namespace WebAPI.Gateway.Persistence
{
    public class LoriotRepo : ILoriotRepo
    {
        public async Task<Temperature> AddTemperatureAsync(Temperature temperature)
        {
            try
            {
                await using var database = new Database();
                
                var first = await database.Temperatures.FirstOrDefaultAsync(u => u.TimeStamp.Equals(temperature.TimeStamp));

                if (first == null)
                {
                    return null;
                }
                await database.Temperatures.AddAsync(temperature);
                await database.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return temperature;
            
        }

        public async Task AddTemperatureAsync(List<Temperature> temperatures)
        {
            foreach (var item in temperatures)
            {
                await AddTemperatureAsync(item);
            }
        }
    }
}