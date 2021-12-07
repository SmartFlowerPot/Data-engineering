using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Persistence
{
    public interface IHumidityRepo
    {
        Task<Humidity> GetHumidityAsync();
        Task<Humidity> GetHumidityAsync(string eui);
    }
}