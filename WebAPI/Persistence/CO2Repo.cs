﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Persistence.Interface;

namespace WebAPI.Persistence
{
    public class CO2Repo:ICO2Repo
    {
        // public async Task<COTwo> GetCO2Async()
        // {
        //     try
        //     {
        //         await using var database = new Database();
        //         //var t = await database.CoTwos.FirstOrDefaultAsync();
        //         return t;
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine(e);
        //     }
        //     return null;
        // }
        //
        // public async Task PostCO2Async(COTwo co2) {
        //     await using var database = new Database();
        //     database.CoTwos.Add(co2);
        //     await database.SaveChangesAsync();
        // }
        //
        // public async Task DeleteHumidityAsync(object validEui)
        // {
        //     await using var database = new Database();
        //     var _cO2 = await database.CoTwos.FirstAsync(c => c.EUI.Equals(validEui));
        //     database.CoTwos.Remove(_cO2);
        //     await database.SaveChangesAsync();
        // }
        
        public async Task<IList<COTwo>> GetListOfCo2Async(string eui)
        {
            DateTime dateTime = DateTime.Now.AddDays(-7);
            await using var database = new Database();
            // var co2 = database.CoTwos.Where(t => t.EUI.Equals(eui))
            //     .Where(t => DateTime.Compare(t.TimeStamp, dateTime) >= 0).ToList();
            // if (!co2.Any())
            //     throw new Exception(Status.MeasurementNotFound);
            var plant = await database.Plants.Include(p => p.Measurements)
                .FirstOrDefaultAsync(p => p.EUI.Equals(eui));
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
            //var t = database.CoTwos.Where(c => c.EUI.Equals(eui)).ToList().LastOrDefault();
            var plant = await database.Plants.Include(p => p.Measurements)
                .FirstOrDefaultAsync(p => p.EUI.Equals(eui));
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