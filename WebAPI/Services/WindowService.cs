using System.Threading.Tasks;
using WebAPI.Controllers;
using WebAPI.Gateway;
using WebAPI.Persistence.Interface;
using WebAPI.Services.Interface;

namespace WebAPI.Services
{
    public class WindowService : IWindowService
    {
        private readonly IPlantRepo _repo;

        public WindowService(IPlantRepo plantRepo)
        {
            _repo = plantRepo;
        }

        public async Task ControlWindow(string eui, int toOpen)
        {
            var plant = await _repo.GetPlantByDeviceAsync(eui);
            
            LoriotClient client = LoriotClient.Instance;
            client.SendDownLinkMessage(eui, toOpen);
        }
    }
}