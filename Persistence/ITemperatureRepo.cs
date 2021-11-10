using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Persistence
{
    public interface ITemperatureRepo
    {
        Task<Temperature> GetTemperatureAsync();
    }
}