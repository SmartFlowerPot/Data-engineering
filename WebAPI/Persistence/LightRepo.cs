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
    public class LightRepo: ILightRepo
    {
        public async Task<Light> GetLightAsync(string eui)
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
            
            return new Light()
            {
                Id = measurement.Id,
                LightLevel = measurement.Light,
                EUI = eui,
                TimeStamp = measurement.TimeStamp
            };
            
        }

        public async Task<IList<Light>> GetListLightAsync(string eui)
        {
            DateTime dateTime = DateTime.Now.AddDays(-7);
            await using var database = new Database();
            var plant = await database.Plants.Include(p => p.Measurements)
                .FirstOrDefaultAsync(p => p.EUI.Equals(eui));
            var measurements = plant.Measurements
                .Where(m => DateTime.Compare(m.TimeStamp, dateTime) >= 0).ToList();
            if (!measurements.Any())
                throw new Exception(Status.MeasurementNotFound);
            return measurements.Select(m => new Light() 
                    {LightLevel = m.Light, EUI = eui, TimeStamp = m.TimeStamp, Id = m.Id})
                .ToList();
        }
    }
}