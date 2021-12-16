using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Persistence;
using WebAPI.Persistence.Interface;
using WebAPI.Services.Interface;

namespace WebAPI.Services
{
    public class LightService: ILightService
    {
        private readonly ILightRepo _lightRepo;

        public LightService(ILightRepo lightRepo)
        {
            _lightRepo = lightRepo;
        }
        
        public async Task<Light> GetLightAsync(string eui)
        {
            return await _lightRepo.GetLightAsync(eui);
        }

        public async Task<IList<Light>> GetListOfLightAsync(string eui)
        {
            return await _lightRepo.GetListLightAsync(eui);
        }
    }
}