using System.Collections.Generic;
using WebAPI.Gateway.Model;
using WebAPI.Models;
using WebAPI.Persistence;

namespace WebAPI.Gateway.Service
{
    public class LoriotService : ILoriotService
    {
        private readonly ITemperatureRepo _temperatureRepo;
        private LoriotClient _client;
        
        public LoriotService(ITemperatureRepo temperatureRepo, LoriotClient loriotClient)
        {
            _temperatureRepo = temperatureRepo;
            _client = loriotClient;
        }
        //Handle message switches through different cmds and based on the port number creates a proper measurement 
        public void HandleMessage(IoTMessage message)
        {
            switch (message.cmd)
            {
                case "rx":
                {
                    if (message.port == 2)
                    {
                        _temperatureRepo.AddTemperatureAsync(CreateTemperature(message));
                    }
                    break;
                }
                case "cq":
                {
                    if (message.port == 2)
                    {
                        foreach (var msg in message.cache)
                        {
                            _temperatureRepo.AddTemperatureAsync(CreateTemperature(msg));
                        }
                    }
                    break;
                }
                case "gw":
                {
                    if (message.port == 2)
                    {
                        foreach (var msg in message.cache)
                        {
                            _temperatureRepo.AddTemperatureAsync(CreateTemperature(msg));
                        }
                    }
                    break; 
                }
            }
        }

        public void SendDownLink()
        {
            _client.SendDownLinkMessage("");
        }

        private Temperature CreateTemperature(IoTMessage message)
        {
            return new()
            {
                TimeStamp = message.ts.ToString(),
                Data = message.data,
                EUI = message.EUI
            }; 
        }
    }
}