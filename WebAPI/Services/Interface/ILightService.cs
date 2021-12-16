using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services.Interface
{
    public interface ILightService
    {
        Task<Light> GetLightAsync(string eui);
        Task<IList<Light>> GetListOfLightAsync(string eui);
    }
}