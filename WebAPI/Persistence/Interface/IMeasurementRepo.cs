using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Persistence.Interface
{
    public interface IMeasurementRepo
    {
        Task AddMeasurementAsync(Measurement measurement);
        Task<Measurement> GetMeasurementAsync(string eui);
        Task<List<Measurement>> GetMeasurementHistoryAsync(string eui);
    }
}