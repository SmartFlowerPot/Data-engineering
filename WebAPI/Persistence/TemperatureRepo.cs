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
        
        public async Task<Temperature> GetTemperatureAsync(string eui)
        {
            await using var database = new Database();
            var plant = await database.Plants.Include(p => p.Measurements)
                .FirstOrDefaultAsync(p => p.EUI.Equals(eui));
            if (plant == null)
            {
                throw new Exception(Status.DeviceNotFound);
            }
            var measurement = plant.Measurements.LastOrDefault();
            
            if (measurement == null)
            {
                throw new Exception(Status.MeasurementNotFound);
            }
            
            var temp = new Temperature()
            {
                Id = measurement.Id,
                TemperatureInDegrees = measurement.Temperature,
                EUI = eui,
                TimeStamp = measurement.TimeStamp
            };

            return temp;
            
        }
        
        public async Task<IList<Temperature>> GetListOfTemperaturesAsync(string eui)
        {
            DateTime dateTime = DateTime.Now.AddDays(-7);
            await using var database = new Database();
            var plant = await database.Plants.Include(p => p.Measurements)
                .FirstOrDefaultAsync(p => p.EUI.Equals(eui));
            
            if (plant == null)
            {
                throw new Exception(Status.DeviceNotFound);
            }
            
            var measurements = plant.Measurements
                .Where(m => DateTime.Compare(m.TimeStamp, dateTime) >= 0).ToList();

            if (!measurements.Any())
                throw new Exception(Status.MeasurementNotFound);
            
            return measurements.Select(m => new Temperature() {TemperatureInDegrees = m.Temperature, EUI = eui,
                    TimeStamp = m.TimeStamp,Id = m.Id,}).ToList();
        }
    }
}