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
    }
}