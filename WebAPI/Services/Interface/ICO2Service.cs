using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services.Interface
{
    public interface ICO2Service
    {
        Task<COTwo> GetCO2Async(string eui);
        Task<IList<COTwo>> GetListOfCo2Async(string eui);

    }
}