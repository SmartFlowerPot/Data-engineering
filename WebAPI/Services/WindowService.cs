using System.Threading.Tasks;
using WebAPI.Controllers;
using WebAPI.Gateway;
using WebAPI.Services.Interface;

namespace WebAPI.Services
{
    public class WindowService : IWindowService
    {
        public void ControlWindow(WindowController.WindowControl windowControl)
        {
            LoriotClient client = LoriotClient.Instance;
            client.SendDownLinkMessage(windowControl);
        }
    }
}