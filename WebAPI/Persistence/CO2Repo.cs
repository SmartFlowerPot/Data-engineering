using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess;
using WebAPI.Models;

namespace WebAPI.Persistence
{
    public class CO2Repo:ICO2Repo
    {
        public async Task<COTwo> GetCO2Async()
        {
            try
            {
                await using var database = new Database();
                var t = await database.CoTwos.FirstOrDefaultAsync();
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