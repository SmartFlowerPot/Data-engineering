using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class TemperatureRepo: ITemperatureRepo
    {
        private Database database;
        
        public TemperatureRepo(Database dbContext)
        {
            database = dbContext;
        }
        
        public async Task<Temperature> GetTemperatureAsync()
        {
            try
            {
                Temperature t = await database.Temperatures.LastOrDefaultAsync();
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