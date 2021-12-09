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
                    // var temp = _processor.CreateTemperature(message);
                    // var humidity = _processor.CreateHumidity(message);
                    // var co2 = _processor.CreateCo2(message);
                    //
                    // await _loriotRepo.AddTemperatureAsync(temp);
                    // await _loriotRepo.AddHumidityAsync(humidity);
                    // await _loriotRepo.AddCo2Async(co2);
                    var measurement = _processor.CreateMeasurement(message);
                    await _loriotRepo.AddMeasurement(measurement, message.EUI);
                    break;
                }
            }
        }
    }
}