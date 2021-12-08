using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public interface ICO2Service
    {
        Task<COTwo> GetCO2Async();
        Task<COTwo> GetCO2Async(string eui);
        
        Task<IList<COTwo>> GetListOfCo2Async(string eui);

    }
}