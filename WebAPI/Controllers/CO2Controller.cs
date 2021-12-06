using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<COTwo>> GetCO2Async()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                COTwo co2 = await _co2Service.GetCO2Async();
                return Ok(co2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}