using WebAPI.Gateway.Model;

namespace WebAPI.Gateway.Service
{
    public interface ILoriotService
    {
        public void HandleMessage(IoTMessage message);
        void SendDownLink();
    }
}