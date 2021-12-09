using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Measurement
    {
        [Key]
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public decimal CO2 { get; set; }

        public override string ToString()
        {
            return $"MEASUREMENT => TIMESTAMP = {TimeStamp}," +
                   $" TEMPERATURE = {Temperature}, HUMIDITY = {Humidity}, CO2 = {CO2}";
        }
    }
}