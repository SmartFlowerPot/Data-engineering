using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Temperature
    {
        //TODO Class was modified for testing purposes!!!
        [Key]
        public DateTime TimeStamp { get; set; }
        [Column("Temperature")]
        public double TemperatureDegrees { get; set; }
        public string Data { get; set; }
        public string EUI { get; set; }

        public override string ToString()
        {
            return $"TEMPERATURE: TIMESTAMP: {TimeStamp}, DATA: {Data}, EUI: {EUI}";
        }
    }
}