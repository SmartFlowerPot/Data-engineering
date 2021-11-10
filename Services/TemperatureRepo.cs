using System;
using System.Threading.Tasks;
using WebAPI.DataAccess;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class TemperatureRepo: ITemperatureRepo
    {
        private DbContext DbContext;
        
        public TemperatureRepo(DbContext dbContext)
        {
            DbContext = dbContext;
        }
        
        public async Task<Temperature> GetTemperatureAsync()
        {
            try
            {
                //Temperature t = await DbContext.
                //return Ok(t);
                Temperature t = new Temperature();
                return t;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }
    }
}