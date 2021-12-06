using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Humidity
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime TimeStamp { get; set; }
        
        public string EUI { get; set; }
        
        public decimal RelativeHumidity { get; set; }

        public override string ToString()
        {
            return $"HUMIDITY: TIMESTAMP: {TimeStamp}, HUMIDITY: {RelativeHumidity}";
        }
    }
}