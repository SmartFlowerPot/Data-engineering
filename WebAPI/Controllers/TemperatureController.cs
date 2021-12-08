using System;
using System.Collections.Generic;
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
                var t = await _temperatureService.GetTemperatureAsync(eui);
                return Ok(t);
            }
            catch (Exception e)
            {
                return HandleException(e.Message);
            }
        }
        
        [HttpGet]
        [Route("history")]
        public async Task<ActionResult<IList<Temperature>>> GetListOfTemperatures([FromQuery] string eui)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var t = await _temperatureService.GetListOfTemperaturesAsync(eui);
                return Ok(t);
            }
            catch (Exception e)
            {
                return e.Message.Equals(Status.MeasurementNotFound) ? NotFound(e.Message) : StatusCode(500, e.Message);
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