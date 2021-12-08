using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Gateway;
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
        
        public async Task<Temperature> GetTemperatureAsync()
        {
            return await _temperatureRepo.GetTemperatureAsync();
        }

        public async Task<Temperature> GetTemperatureAsync(string eui)
        {
            return await _temperatureRepo.GetTemperatureAsync(eui);

        }

        public async Task<IList<Temperature>> GetListOfTemperaturesAsync(string eui)
        {
            return await _temperatureRepo.GetListOfTemperaturesAsync(eui);
        }
    }
}