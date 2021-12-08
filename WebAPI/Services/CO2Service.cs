using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Persistence;

namespace WebAPI.Services
{
    public class CO2Service : ICO2Service
    {
        private readonly ICO2Repo _co2Repo;

        public CO2Service(ICO2Repo ico2Repo)
        {
            _co2Repo = ico2Repo;
        }
        
        public async Task<COTwo> GetCO2Async()
        {
            return await _co2Repo.GetCO2Async();
        }

        public async Task<COTwo> GetCO2Async(string eui)
        {
            return await _co2Repo.GetCO2Async(eui);

        }
        public async Task<IList<COTwo>> GetListOfCo2Async(string eui)
        {
            return await _co2Repo.GetListOfCo2Async(eui);
        }
    }
}