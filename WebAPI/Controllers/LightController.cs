using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Services.Interface;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LightController: ControllerBase
    {
        private readonly ILightService _service;

        public LightController(ILightService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<ActionResult<Light>> GetLightAsync([FromQuery] string eui)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Light light = await _service.GetLightAsync(eui);
                return Ok(light);
            }
            catch (Exception e)
            {
                return HandleException(e.Message);
            }
        }
        
        [HttpGet]
        [Route("history")]
        public async Task<ActionResult<IList<Light>>> GetListOfLights([FromQuery] string eui)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var t = await _service.GetListOfLightAsync(eui);
                return Ok(t);
            }
            catch (Exception e)
            {
                return e.Message.Equals(Status.MeasurementNotFound) ? NotFound(e.Message) : StatusCode(500, e.Message);
            }
        }
        
        private ActionResult<Light> HandleException(string message)
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