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
    public class CO2Controller : ControllerBase
    {
        private readonly ICO2Service _co2Service;

        public CO2Controller(ICO2Service co2Service)
        {
            _co2Service = co2Service;
        }
        
        [HttpGet]
        public async Task<ActionResult<COTwo>> GetCO2Async([FromQuery] string eui)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                COTwo co2 = await _co2Service.GetCO2Async(eui);
                return Ok(co2);
            }
            catch (Exception e)
            {
                return HandleException(e.Message);
            }
        }
        
        private ActionResult<COTwo> HandleException(string message)
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