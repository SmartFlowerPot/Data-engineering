using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface ITemperatureService
    {
        Task<Temperature> GetTemperatureAsync();
        Task<Temperature> GetTemperatureAsync(string eui);
    }
}