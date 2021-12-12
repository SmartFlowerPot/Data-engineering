using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Exceptions;
using WebAPI.Services.Interface;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WindowController : ControllerBase
    {
        private readonly IWindowService _windowService;

        public WindowController(IWindowService service)
        {
            _windowService = service;
        }
        
        [HttpPost]
        public async Task<ActionResult> ControlWindow([FromQuery] string eui, bool toOpen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _windowService.ControlWindow(eui, toOpen);
                return Accepted();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return e.Message.Equals(Status.DeviceNotFound) ? NotFound(e.Message) : StatusCode(500, e.Message);

            }
            
        }
    }
}