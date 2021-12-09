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
    public class HumidityRepo : IHumidityRepo
    {
        // public async Task<Humidity> GetHumidityAsync()
        // {
        //     try
        //     {
        //         await using var database = new Database();
        //         //var t = await database.Humidities.FirstOrDefaultAsync();
        //         return t;
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine(e);
        //     }
        //     return null;
        // }

        public async Task<Humidity> GetHumidityAsync(string eui)
        {
            await using var database = new Database();
            //var t = database.Humidities.Where(hum => hum.EUI.Equals(eui)).ToList().LastOrDefault();
            // if (t == null)
            // {
            //     throw new Exception(Status.MeasurementNotFound);
            // }
            // return t;
            var plant = await database.Plants.Include(p => p.Measurements)
                .FirstOrDefaultAsync(p => p.EUI.Equals(eui));
            var measurement = plant.Measurements.LastOrDefault();
            if (measurement == null)
            {
                throw new Exception(Status.MeasurementNotFound);
            }

            var humidity = new Humidity()
            {
                Id = measurement.Id,
                RelativeHumidity = measurement.Humidity,
                EUI = eui,
                TimeStamp = measurement.TimeStamp
            };

            return humidity;
        }

        // public async Task PostHumidityAsync(Humidity humidity)
        // {
        //     await using var database = new Database();
        //     //database.Humidities.Add(humidity);
        //     await database.SaveChangesAsync();
        // }
        //
        // public async Task DeleteHumidityAsync(string eui)
        // {
        //     await using var database = new Database();
        //     var humidity = await database.Humidities.FirstAsync(h => h.EUI.Equals(eui));
        //     database.Humidities.Remove(humidity);
        //     await database.SaveChangesAsync();
        // }

        public async Task<IList<Humidity>> GetListOfHumidityAsync(string eui)
        {
            DateTime dateTime = DateTime.Now.AddDays(-7);
            await using var database = new Database();
            // var humidities = database.Humidities.Where(t => t.EUI.Equals(eui))
            //     .Where(t => DateTime.Compare(t.TimeStamp, dateTime) >= 0).ToList();
            // if (!humidities.Any())
            //     throw new Exception(Status.MeasurementNotFound);
            // return humidities;
            var plant = await database.Plants.Include(p => p.Measurements)
                .FirstOrDefaultAsync(p => p.EUI.Equals(eui));
            var measurements = plant.Measurements
                .Where(m => DateTime.Compare(m.TimeStamp, dateTime) >= 0).ToList();

            if (!measurements.Any())
                throw new Exception(Status.MeasurementNotFound);
            return measurements.Select(m => new Humidity()
                    {RelativeHumidity = m.CO2, EUI = eui, TimeStamp = m.TimeStamp, Id = m.Id})
                .ToList();
        }
    }
}