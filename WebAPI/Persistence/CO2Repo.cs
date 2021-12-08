using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess;
using WebAPI.Exceptions;
using WebAPI.Models;

namespace WebAPI.Persistence
{
    public class CO2Repo:ICO2Repo
    {
        public async Task<COTwo> GetCO2Async()
        {
            try
            {
                await using var database = new Database();
                var t = await database.CoTwos.FirstOrDefaultAsync();
                return t;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }
        
        public async Task PostCO2Async(COTwo co2) {
            await using var database = new Database();
            database.CoTwos.Add(co2);
            await database.SaveChangesAsync();
        }

        public async Task DeleteHumidityAsync(object validEui)
        {
            await using var database = new Database();
            var _cO2 = await database.CoTwos.FirstAsync(c => c.EUI.Equals(validEui));
            database.CoTwos.Remove(_cO2);
            await database.SaveChangesAsync();
        }

        public async Task<COTwo> GetCO2Async(string eui)
        {
            await using var database = new Database();
            var t = database.CoTwos.Where(c => c.EUI.Equals(eui)).ToList().LastOrDefault();
            if (t == null)
            {
                throw new Exception(Status.MeasurementNotFound);
            }
            return t;
        }
        
    }
}