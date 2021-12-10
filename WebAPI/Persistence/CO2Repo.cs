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
    public class CO2Repo:ICO2Repo
    {
        public async Task<IList<COTwo>> GetListOfCo2Async(string eui)
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
            return measurements.Select(m => new COTwo() 
                    {CO2Level = m.CO2, EUI = eui, TimeStamp = m.TimeStamp, Id = m.Id})
                .ToList();
        }
        
        public async Task<COTwo> GetCO2Async(string eui)
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
            var co2 = new COTwo()
            {
                Id = measurement.Id,
                CO2Level = measurement.CO2,
                EUI = eui,
                TimeStamp = measurement.TimeStamp
            };
            return co2;
        }
    }
}