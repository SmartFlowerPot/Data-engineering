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
    public class MeasurementController: ControllerBase
    {
        private readonly IMeasurementService _measurementService;
        
        public MeasurementController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }
        
        [HttpGet]
        public async Task<ActionResult<Measurement>> GetLastMeasurement([FromQuery] string eui)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var t = await _measurementService.GetMeasurementAsync(eui);
                return Ok(t);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return e.Message.Equals(Status.MeasurementNotFound) ? NotFound(e.Message) : StatusCode(500, e.Message);
            }
        }
        
        [HttpGet]
        [Route("history")]
        public async Task<ActionResult<IList<Measurement>>> GetListOfMeasurements([FromQuery] string eui)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var t = await _measurementService.GetMeasurementHistoryAsync(eui);
                return Ok(t);
            }
            catch (Exception e)
            {
                return e.Message.Equals(Status.MeasurementNotFound) ? NotFound(e.Message) : StatusCode(500, e.Message);
            }
        }

    }
}