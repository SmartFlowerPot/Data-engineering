using System.Threading.Tasks;
using WebAPI.Gateway.Model;
using WebAPI.Models;

namespace WebAPI.Gateway.Service
{
    public interface ILoriotService
    {
        public Task HandleMessage(IoTMessage message);
        
    }
}