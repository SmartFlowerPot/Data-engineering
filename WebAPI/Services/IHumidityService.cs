using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface IHumidityService
    {
        Task<Humidity> GetHumidityAsync();
    }
}