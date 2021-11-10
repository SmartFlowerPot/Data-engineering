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
        private readonly ITemperatureRepo _temperatureRepo;

        public TemperatureController(ITemperatureRepo temperatureRepo)
        {
            _temperatureRepo = temperatureRepo;
        }
        

        [HttpGet]
        public async Task<ActionResult<Temperature>> GetTemperatureAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Temperature t = await _temperatureRepo.GetTemperatureAsync();
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