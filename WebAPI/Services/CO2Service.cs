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
    }
}