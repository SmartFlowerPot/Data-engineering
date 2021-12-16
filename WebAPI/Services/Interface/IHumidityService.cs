using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services.Interface
{
    public interface IHumidityService
    {
        Task<Humidity> GetHumidityAsync(string eui);
        
        Task<IList<Humidity>> GetListOfHumidityAsync(string eui);

    }
}