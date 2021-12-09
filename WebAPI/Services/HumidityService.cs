using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Persistence;
using WebAPI.Persistence.Interface;
using WebAPI.Services.Interface;

namespace WebAPI.Services
{
    public class HumidityService : IHumidityService
    {
        private readonly IHumidityRepo _repo;

        public HumidityService(IHumidityRepo repo)
        {
            _repo = repo;
        }
        public async Task<Humidity> GetHumidityAsync(string eui)
        {
            return await _repo.GetHumidityAsync(eui);

        }

        public async Task<IList<Humidity>> GetListOfHumidityAsync(string eui)
        {
            return await _repo.GetListOfHumidityAsync(eui);
        }
    }
}