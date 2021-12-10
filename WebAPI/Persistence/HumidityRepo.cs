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
        public async Task<Humidity> GetHumidityAsync(string eui)
        {
            await using var database = new Database();
            var plant = await database.Plants.Include(p => p.Measurements)
                .FirstOrDefaultAsync(p => p.EUI.Equals(eui));
            
            if (plant == null)
            {
                throw new Exception(Status.PlantNotFound);
            }
            
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
        
        public async Task<IList<Humidity>> GetListOfHumidityAsync(string eui)
        {
            DateTime dateTime = DateTime.Now.AddDays(-7);
            await using var database = new Database();
            var plant = await database.Plants.Include(p => p.Measurements)
                .FirstOrDefaultAsync(p => p.EUI.Equals(eui));
            
            if (plant == null)
            {
                throw new Exception(Status.PlantNotFound);
            }
            
            var measurements = plant.Measurements
                .Where(m => DateTime.Compare(m.TimeStamp, dateTime) >= 0).ToList();

            if (!measurements.Any())
                throw new Exception(Status.MeasurementNotFound);
            return measurements.Select(m => new Humidity()
                    {RelativeHumidity = m.Humidity, EUI = eui, TimeStamp = m.TimeStamp, Id = m.Id})
                .ToList();
        }
    }
}