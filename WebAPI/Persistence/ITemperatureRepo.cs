using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Persistence
{
    public interface ITemperatureRepo
    {
        Task<Temperature> GetTemperatureAsync();
        Task<Temperature> AddTemperatureAsync(Temperature temperature);
        Task AddTemperatureAsync(List<Temperature> temperatures);
    }
}