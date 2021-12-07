using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess;
using WebAPI.Exceptions;
using WebAPI.Models;

namespace WebAPI.Persistence
{
    public class HumidityRepo : IHumidityRepo
    {
        public async Task<Humidity> GetHumidityAsync()
        {
            try
            {
                await using var database = new Database();
                var t = await database.Humidities.FirstOrDefaultAsync();
                return t;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        public async Task<Humidity> GetHumidityAsync(string eui)
        {
            await using var database = new Database();
            var t = database.Humidities.Where(hum => hum.EUI.Equals(eui)).ToList().LastOrDefault();
            if (t == null)
            {
                throw new Exception(Status.MeasurementNotFound);
            }
            return t;
        }

        public async Task PostHumidityAsync(Humidity humidity)
        {
            await using var database = new Database();
            database.Humidities.Add(humidity);
            await database.SaveChangesAsync();
        }

        public async Task DeleteHumidityAsync(string eui)
        {
            await using var database = new Database();
            var humidity = await database.Humidities.FirstAsync(h => h.EUI.Equals(eui));
            database.Humidities.Remove(humidity);
            await database.SaveChangesAsync();
        }
    }
}