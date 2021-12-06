using System;
using System.Threading.Tasks;
using WebAPI.DataAccess;
using WebAPI.Models;

namespace WebAPI.Gateway.Persistence
{
    public class LoriotRepo : ILoriotRepo
    {
        public async Task AddTemperatureAsync(Temperature temperature)
        {
            try
            {
                await using var database = new Database();
                
                await database.Temperatures.AddAsync(temperature);
                await database.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task AddHumidityAsync(Humidity humidity)
        {
            await using var database = new Database();

            await database.Humidities.AddAsync(humidity);
            await database.SaveChangesAsync();
        }

        public async Task AddCo2Async(COTwo co2)
        {
            await using var database = new Database();

            await database.CoTwos.AddAsync(co2);
            await database.SaveChangesAsync();
        }
    }
}