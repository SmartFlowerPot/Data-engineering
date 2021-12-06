using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess;
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
    }
}