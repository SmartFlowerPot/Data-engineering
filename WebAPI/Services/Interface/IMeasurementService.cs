using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services.Interface
{
    public interface IMeasurementService
    {
        Task AddMeasurementAsync(Measurement measurement);
        Task<Measurement> GetMeasurementAsync(string eui);
        Task<List<Measurement>> GetMeasurementHistoryAsync(string eui);
    }
}