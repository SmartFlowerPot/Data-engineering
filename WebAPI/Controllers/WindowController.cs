using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public void ControlWindow([FromBody] WindowControl control)
        {
            _windowService.ControlWindow(control);
        }

        public class WindowControl
        {
            public string EUI { get; set; }
            public DateTime TimeStamp { get; set; }
            public bool OpenedClosed { get; set; }
        }
    }
}