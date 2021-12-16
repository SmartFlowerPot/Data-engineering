using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Persistence.Interface
{
    public interface IHumidityRepo
    {
        Task<Humidity> GetHumidityAsync(string eui);
        Task<IList<Humidity>> GetListOfHumidityAsync(string eui);

    }
}