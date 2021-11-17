using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Gateway.Persistence
{
    public interface ILoriotRepo
    {
        Task<Temperature> AddTemperatureAsync(Temperature temperature);
        Task AddTemperatureAsync(List<Temperature> temperatures);
    }
}