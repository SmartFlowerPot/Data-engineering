using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Gateway.Persistence
{
    public interface ILoriotRepo
    {
        Task AddTemperatureAsync(Temperature temperature);
        Task AddHumidityAsync(Humidity humidity);
        Task AddCo2Async(COTwo co2);
    }
}