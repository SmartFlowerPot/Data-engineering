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
        private readonly ITemperatureService _temperatureService;

        public TemperatureController(ITemperatureService temperatureService)
        {
            _temperatureService = temperatureService;
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
                Temperature t = await _temperatureService.GetTemperatureAsync();
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