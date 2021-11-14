using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess;
using WebAPI.Models;

namespace WebAPI.Persistence
{
    public class TemperatureRepo: ITemperatureRepo
    {
        public async Task<Temperature> GetTemperatureAsync()
        {
            try
            {
                await using var database = new Database();
                var t = await database.Temperatures.FirstOrDefaultAsync();
                return t;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        public async Task<Temperature> AddTemperatureAsync(Temperature temperature)
        {
            // try
            // {
            //     await using var database = new Database();
            //     await database.Temperatures.AddAsync(temperature);
            //     await database.SaveChangesAsync();
            // }
            // catch (Exception e)
            // {
            //     Console.WriteLine(e);
            // }
            // return null;

            Console.WriteLine($"TEMPERATURE REPO: Timestamp: {temperature.TimeStamp}, Data: {temperature.Data}, EUI: {temperature.EUI}");
            return null;
        }
    }
}