using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlantController : ControllerBase
    {
        private readonly IPlantService _plantService;

        public PlantController(IPlantService plantService)
        {
            _plantService = plantService;
        }
        
        [HttpPost]
        [Route("{username}")]
        public async Task<ActionResult<Plant>> PostPlantAsync([FromBody] Plant plant, [FromRoute] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Status.BadRequest);
            }
            try
            {
                var added = await _plantService.PostPlantAsync(plant, username);
                return Created($"/{added.Nickname}", added);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        
        // get specific plant by device eui
        // eg: /plant?eui=0004A30B00251001
        [HttpGet]
        public async Task<ActionResult<Plant>> GetPlantByDeviceAsync([FromQuery] string eui)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Status.BadRequest);
            }
            try
            {
                var plant = await _plantService.GetPlantByDeviceAsync(eui);
                return Ok(plant);
            }
            catch (Exception e)
            {
                return e.Message == Status.DeviceNotFound ? NotFound(e.Message) : StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<Plant>> DeletePlantAsync([FromQuery] string eui)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Status.BadRequest);
            }
            try
            {
                var plant = await _plantService.DeletePlantAsync(eui);
                return Ok(plant);
            }
            catch (Exception e)
            {
                return e.Message == Status.PlantNotFound ? NotFound(e.Message) : StatusCode(500, e.Message);
            }
        }
    }
}