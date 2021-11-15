using System;
using System.Collections.Generic;
using WebAPI.Gateway.Model;
using WebAPI.Models;
using WebAPI.Persistence;

namespace WebAPI.Gateway.Service
{
    public class LoriotService : ILoriotService
    {
       private readonly ITemperatureRepo _temperatureRepo;
       
        
        public LoriotService(ITemperatureRepo temperatureRepo)
        {
            _temperatureRepo = temperatureRepo;
            
        }
        //Handle message switches through different cmds and based on the port number creates a proper measurement
        //Port number 1 => Temperature reading
        //TODO Method to convert actual data to proper value, need to agree with the IoT team
        public void HandleMessage(IoTMessage message)
        {
            List<Temperature> temperatures = new List<Temperature>();
            switch (message.cmd)
            {
                case "rx":
                {
                    if (message.port == 1)
                    {
                        temperatures.Add(CreateTemperature(message));
                        //_temperatureRepo.AddTemperatureAsync(CreateTemperature(message));
                    }
                    break;
                }
                case "cq":
                {
                    foreach (var msg in message.cache)
                    {
                        if (msg.port == 1)
                        {
                            temperatures.Add(CreateTemperature(msg));
                            // _temperatureRepo.AddTemperatureAsync(CreateTemperature(msg));
                        }
                    }
                    break;
                }
                case "gw":
                {
                    foreach (var msg in message.cache)
                    {
                        if (msg.port == 1)
                        {
                            temperatures.Add(CreateTemperature(msg));
                            //_temperatureRepo.AddTemperatureAsync(CreateTemperature(msg));
                        }
                    }
                    break; 
                }
            }

            _temperatureRepo.AddTemperatureAsync(temperatures);
        }
        
        private Temperature CreateTemperature(IoTMessage message)
        {
            // DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            // DateTime timeStamp = start.AddMilliseconds(message.ts).ToLocalTime();
            
            return new()
            {
                // DateTime = message.ts.ToString(),
                TimeStamp = DateTimeOffset.FromUnixTimeMilliseconds(message.ts).DateTime,
                Data = message.data,
                EUI = message.EUI
            }; 
        }
    }
}