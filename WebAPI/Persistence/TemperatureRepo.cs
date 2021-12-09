using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Persistence.Interface;

namespace WebAPI.Persistence
{
    public class TemperatureRepo : ITemperatureRepo
    {
        // public async Task<Temperature> GetTemperatureAsync()
        // {
        //     try
        //     {
        //         await using var database = new Database();
        //         var t = await database.Temperatures.FirstOrDefaultAsync();
        //         return t;
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine(e);
        //     }
        //     return null;
        // }

        public async Task<Temperature> GetTemperatureAsync(string eui)
        {
            await using var database = new Database();
            // var t = database.Temperatures.Where(temp => temp.EUI.Equals(eui)).ToList().LastOrDefault();
            // if (t == null)
            // {
            //     throw new Exception(Status.MeasurementNotFound);
            // }
            //
            // return t;
            var plant = await database.Plants.Include(p => p.Measurements)
                .FirstOrDefaultAsync(p => p.EUI.Equals(eui));
            var measurement = plant.Measurements.LastOrDefault();
            
            if (measurement == null)
            {
                throw new Exception(Status.MeasurementNotFound);
            }
            
            var temp = new Temperature()
            {
                Id = measurement.Id,
                TemperatureInDegrees = measurement.Humidity,
                EUI = eui,
                TimeStamp = measurement.TimeStamp
            };

            return temp;
            
        }

        // public async Task PostTemperatureAsync(Temperature temperature)
        // {
        //     await using var database = new Database();
        //     database.Temperatures.Add(temperature);
        //     await database.SaveChangesAsync();
        // }
        //
        // public async Task DeleteTemperatureAsync(string eui)
        // {
        //     await using var database = new Database();
        //     var temperature = await database.Temperatures.FirstAsync(h => h.EUI.Equals(eui));
        //     database.Temperatures.Remove(temperature);
        //     await database.SaveChangesAsync();
        // }

        public async Task<IList<Temperature>> GetListOfTemperaturesAsync(string eui)
        {
            DateTime dateTime = DateTime.Now.AddDays(-7);
            await using var database = new Database();
            // var temperatures = database.Temperatures.Where(t => t.EUI.Equals(eui))
            //     .Where(t => DateTime.Compare(t.TimeStamp, dateTime) >= 0).ToList();
            // if (!temperatures.Any())
            //     throw new Exception(Status.MeasurementNotFound);
            // return temperatures;
            var plant = await database.Plants.Include(p => p.Measurements)
                .FirstOrDefaultAsync(p => p.EUI.Equals(eui));
            var measurements = plant.Measurements
                .Where(m => DateTime.Compare(m.TimeStamp, dateTime) >= 0).ToList();

            if (!measurements.Any())
                throw new Exception(Status.MeasurementNotFound);
            
            return measurements.Select(m => new Temperature() {TemperatureInDegrees = m.CO2, EUI = eui,
                    TimeStamp = m.TimeStamp,Id = m.Id,}).ToList();
        }
    }
}