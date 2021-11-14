using System.Threading.Tasks;
using WebAPI.Gateway.Service;
using WebAPI.Models;
using WebAPI.Persistence;

namespace WebAPI.Services
{
    public class TemperatureService : ITemperatureService
    {
        private readonly ITemperatureRepo _temperatureRepo;

        public TemperatureService(ITemperatureRepo temperatureRepo)
        {
            _temperatureRepo = temperatureRepo;
        }
        
        public Task<Temperature> GetTemperatureAsync()
        {
            return _temperatureRepo.GetTemperatureAsync();
        }
    }
}