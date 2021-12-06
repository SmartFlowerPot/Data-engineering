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
        
    }
}