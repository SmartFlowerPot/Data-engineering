using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess;
using WebAPI.Exceptions;
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

        public async Task<Temperature> GetTemperatureAsync(string eui)
        {
            await using var database = new Database();
                var t = database.Temperatures.Where(temp => temp.EUI.Equals(eui)).ToList().LastOrDefault();
                if (t == null)
                {
                    throw new Exception(Status.MeasurementNotFound);
                }

                return t;
        }
            
    }
}