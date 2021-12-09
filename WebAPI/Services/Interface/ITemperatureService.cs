using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services.Interface
{
    public interface ITemperatureService
    {
        Task<Temperature> GetTemperatureAsync(string eui);
        Task<IList<Temperature>> GetListOfTemperaturesAsync(string eui);
    }
}