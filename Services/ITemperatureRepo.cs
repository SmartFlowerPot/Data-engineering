using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface ITemperatureRepo
    {
        Task<Temperature> GetTemperatureAsync();
    }
}