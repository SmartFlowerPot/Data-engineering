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
        
        [HttpGet]
        [Route("history")]
        public async Task<ActionResult<IList<COTwo>>> GetListOfTemperatures([FromQuery] string eui)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var t = await _co2Service.GetListOfCo2Async(eui);
                return Ok(t);
            }
            catch (Exception e)
            {
                return e.Message.Equals(Status.MeasurementNotFound) ? NotFound(e.Message) : StatusCode(500, e.Message);
            }
        }
        
        private ActionResult<COTwo> HandleException(string message)
        {
            switch (message)
            {
                case Status.MeasurementNotFound:
                {
                    return NotFound(message);
                }
                case Status.PlantNotFound:
                {
                    return NotFound(message);
                }
            }
            return StatusCode(500, message);
        }
    }
}