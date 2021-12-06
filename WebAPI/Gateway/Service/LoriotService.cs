using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Gateway.Model;
using WebAPI.Gateway.Persistence;
using WebAPI.Models;
using WebAPI.Persistence;

namespace WebAPI.Gateway.Service
{
    public class LoriotService : ILoriotService
    {
       private readonly ILoriotRepo _loriotRepo;
       private MessageProcessor _processor;
       
        
        public LoriotService()
        {
            _loriotRepo = new LoriotRepo();
            _processor = new MessageProcessor();
        }
        //Handle message switches through different cmds and based on the port number creates a proper measurement
        //Port number 1 => Temperature reading
        //TODO Method to process the data to a proper value, need to agree with the IoT team on the format
        public async Task HandleMessage(IoTMessage message)
        {
            switch (message.cmd)
            {
                case "rx":
                {
                    // var temp = CreateTemperature(message);
                    // temperatures.Add(temp);
                    // Console.WriteLine($"HANDLE MESSAGE => {temp}");
                    //_temperatureRepo.AddTemperatureAsync(CreateTemperature(message));

                    var temp = _processor.CreateTemperature(message);
                    var humidity = _processor.CreateHumidity(message);
                    var co2 = _processor.CreateCo2(message);

                    await _loriotRepo.AddTemperatureAsync(temp);
                    await _loriotRepo.AddHumidityAsync(humidity);
                    await _loriotRepo.AddCo2Async(co2);
                    break;
                }
                // case "cq":
                // {
                //     foreach (var msg in message.cache)
                //     {
                //         if (msg.port == 1)
                //         {
                //             temperatures.Add(CreateTemperature(msg));
                //             // _temperatureRepo.AddTemperatureAsync(CreateTemperature(msg));
                //         }
                //     }
                //     break;
                // }
                // case "gw":
                // {
                //     if (message.port == 1)
                //     {
                //         temperatures.Add(CreateTemperature(message));
                //         //_temperatureRepo.AddTemperatureAsync(CreateTemperature(msg));
                //     }
                //     break; 
                // }
            }
        }
    }
}