using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Persistence
{
    public interface ICO2Repo
    {
        Task<COTwo> GetCO2Async();
        
        Task<COTwo> GetCO2Async(string eui);
        Task PostCO2Async(COTwo co2);
        Task DeleteHumidityAsync(object validEui);
        Task<IList<COTwo>> GetListOfCo2Async(string eui);

    }
}