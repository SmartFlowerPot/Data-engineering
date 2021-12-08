using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IHumidityService
    {
        Task<Humidity> GetHumidityAsync();
        Task<Humidity> GetHumidityAsync(string eui);
        
        Task<IList<Humidity>> GetListOfHumidityAsync(string eui);

    }
}