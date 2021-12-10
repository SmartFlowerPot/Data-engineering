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
    public class MeasurementRepo : IMeasurementRepo
    {
        public Task AddMeasurementAsync(Measurement measurement)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Measurement> GetMeasurementAsync(string eui)
        {
            await using var database = new Database();
            var plant = await database.Plants.Include(p => p.Measurements)
                .FirstOrDefaultAsync(p => p.EUI.Equals(eui));
            
            if (plant == null)
            {
                throw new Exception(Status.PlantNotFound);
            }
            
            var measurement = plant.Measurements.LastOrDefault();
            
            if (measurement is null)
            {
                throw new Exception(Status.MeasurementNotFound);
            }
            
            return measurement;
        }

        public async Task<List<Measurement>> GetMeasurementHistoryAsync(string eui)
        {
            var dateTime = DateTime.Now.AddDays(-7);
            
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
            return measurements;
        }
    }
}