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
    public class HumidityController : ControllerBase
    {
        private readonly IHumidityService _service;

        public HumidityController(IHumidityService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<ActionResult<Humidity>> GetHumidityAsync([FromQuery] string eui)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Humidity humidity = await _service.GetHumidityAsync(eui);
                return Ok(humidity);
            }
            catch (Exception e)
            {
                return HandleException(e.Message);
            }
        }
        
        private ActionResult<Humidity> HandleException(string message)
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