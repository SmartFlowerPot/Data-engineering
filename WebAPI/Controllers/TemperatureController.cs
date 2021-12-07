using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Exceptions;
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
        public async Task<ActionResult<Temperature>> GetLastTemperatureAsync([FromQuery] string eui)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Temperature t = await _temperatureService.GetTemperatureAsync(eui);
                return Ok(t);
            }
            catch (Exception e)
            {
                return HandleException(e.Message);
            }
        }

        private ActionResult<Temperature> HandleException(string message)
        {
            switch (message)
            {
                case Status.MeasurementNotFound:
                {
                    return NotFound(message);
                    break;
                }
            }
            return StatusCode(500, message);
        }
    }
}