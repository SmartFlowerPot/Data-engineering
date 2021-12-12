using System.Threading.Tasks;
using WebAPI.Controllers;

namespace WebAPI.Services.Interface
{
    public interface IWindowService
    { 
        Task ControlWindow(string windowControl, bool toOpen);
    }
}