using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Persistence
{
    public interface ITemperatureRepo
    {
        Task<Temperature> GetTemperatureAsync();
        Task<Temperature> GetTemperatureAsync(string eui);
        Task PostTemperatureAsync(Temperature temperature);
        Task DeleteTemperatureAsync(string eui);
        Task<IList<Temperature>> GetListOfTemperaturesAsync(string eui);
    }
}