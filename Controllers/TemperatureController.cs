using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemperatureController: ControllerBase
    {
        private ITemperatureRepo TemperatureRepo;

        public TemperatureController(TemperatureRepo TemperatureRepo)
        {
            this.TemperatureRepo = TemperatureRepo;
        }
        

        [HttpGet]
        public async Task<ActionResult<Temperature>> GetTemperatureAsync()
        {
            try
            {
                Temperature t = await TemperatureRepo.GetTemperatureAsync();
                return Ok(t);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        
    }
}