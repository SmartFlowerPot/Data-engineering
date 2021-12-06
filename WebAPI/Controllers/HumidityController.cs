using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<Humidity>> GetHumidityAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Humidity humidity = await _service.GetHumidityAsync();
                return Ok(humidity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}