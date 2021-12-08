using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Persistence
{
    public interface IHumidityRepo
    {
        Task<Humidity> GetHumidityAsync();
        Task<Humidity> GetHumidityAsync(string eui);
        Task PostHumidityAsync(Humidity humidity);
        Task DeleteHumidityAsync(string eui);
        Task<IList<Humidity>> GetListOfHumidityAsync(string eui);

    }
}