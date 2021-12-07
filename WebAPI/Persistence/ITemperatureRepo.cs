﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Persistence
{
    public interface ITemperatureRepo
    {
        Task<Temperature> GetTemperatureAsync();
        Task<Temperature> GetTemperatureAsync(string eui);
    }
}