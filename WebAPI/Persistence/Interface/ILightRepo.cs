using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Persistence.Interface
{
    public interface ILightRepo
    {
        Task<Light> GetLightAsync(string eui);
        Task<IList<Light>> GetListLightAsync(string eui);
    }
}