using System;
using WebAPI.Gateway.Model;
using WebAPI.Models;

namespace WebAPI.Gateway.Service
{
    public class MessageProcessor
    {

        public Measurement CreateMeasurement(IoTMessage message)
        {
            var measurement = new Measurement();
            measurement.Temperature = CreateTemperature(message).TemperatureInDegrees;
            measurement.Humidity = CreateHumidity(message).RelativeHumidity;
            measurement.CO2 = CreateCo2(message).CO2Level;
            measurement.TimeStamp = DateTimeOffset.FromUnixTimeMilliseconds(message.ts).DateTime;
            return measurement;
        }
        
        public Temperature CreateTemperature(IoTMessage message)
        {
            String hexString = message.data;
            
            //Byte[0]
            int dec = int.Parse(hexString.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            
            //Byte[1]
            int point = int.Parse(hexString.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
            
            Console.WriteLine($"Decimal: {dec} Point: {point}");
            decimal number = (decimal) (dec + (point / 100.0));
            Console.WriteLine($"NUMBAAAAAAA: {number}");
            
            return new()
            {
                TemperatureInDegrees = number,
                TimeStamp = DateTimeOffset.FromUnixTimeMilliseconds(message.ts).DateTime,
                EUI = message.EUI
            }; 
        }

        public Humidity CreateHumidity(IoTMessage message)
        {
            String hexString = message.data;
            //Byte[2]
            int humidity = int.Parse(hexString.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
            return new Humidity()
            {
                EUI = message.EUI,
                TimeStamp = DateTimeOffset.FromUnixTimeMilliseconds(message.ts).DateTime,
                RelativeHumidity = humidity
            };
        }

        public COTwo CreateCo2(IoTMessage message)
        {
            String hexString = message.data;
            int co2Level = int.Parse(hexString.Substring(6), System.Globalization.NumberStyles.HexNumber);
            
            return new()
            {
                EUI = message.EUI,
                TimeStamp = DateTimeOffset.FromUnixTimeMilliseconds(message.ts).DateTime,
                CO2Level = co2Level
            };
        }
    }
}