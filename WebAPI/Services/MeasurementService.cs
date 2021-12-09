using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Persistence;
using WebAPI.Persistence.Interface;
using WebAPI.Services.Interface;

namespace WebAPI.Services
{
    public class MeasurementService : IMeasurementService
    {
        private readonly IMeasurementRepo _repo;

        public MeasurementService(IMeasurementRepo repo)
        {
            _repo = repo;
        }
        public Task AddMeasurementAsync(Measurement measurement)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Measurement> GetMeasurementAsync(string eui)
        {
            return await _repo.GetMeasurementAsync(eui);
        }

        public async Task<List<Measurement>> GetMeasurementHistoryAsync(string eui)
        {
            return await _repo.GetMeasurementHistoryAsync(eui);
        }
    }
}