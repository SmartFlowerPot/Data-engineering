using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Services;
using WebAPI.Services.Interface;

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
        
        [HttpGet]
        [Route("history")]
        public async Task<ActionResult<IList<Humidity>>> GetListOfTemperatures([FromQuery] string eui)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var t = await _service.GetListOfHumidityAsync(eui);
                return Ok(t);
            }
            catch (Exception e)
            {
                return e.Message.Equals(Status.MeasurementNotFound) ? NotFound(e.Message) : StatusCode(500, e.Message);
            }
        }
        
        private ActionResult<Humidity> HandleException(string message)
        {
            switch (message)
            {
                case Status.MeasurementNotFound:
                {
                    return NotFound(message);
                }
                case Status.DeviceNotFound:
                {
                    return NotFound(message);
                }
            }
            return StatusCode(500, message);
        }
    }
}