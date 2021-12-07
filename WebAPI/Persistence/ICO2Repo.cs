using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Persistence
{
    public interface ICO2Repo
    {
        Task<COTwo> GetCO2Async();
        
        Task<COTwo> GetCO2Async(string eui);
    }
}