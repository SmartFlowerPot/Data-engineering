using WebAPI.Gateway.Model;
using WebAPI.Models;

namespace WebAPI.Gateway.Service
{
    public interface ILoriotService
    {
        public void HandleMessage(IoTMessage message);
        
    }
}