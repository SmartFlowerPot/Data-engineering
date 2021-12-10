using System.Threading.Tasks;
using WebAPI.Controllers;

namespace WebAPI.Services.Interface
{
    public interface IWindowService
    {
        void ControlWindow(WindowController.WindowControl windowControl);
    }
}