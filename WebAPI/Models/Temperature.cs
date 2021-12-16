using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Temperature
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime TimeStamp { get; set; }
        
        [Column("Temperature")]
        public decimal TemperatureInDegrees { get; set; }
        
        public string EUI { get; set; }

        public override string ToString()
        {
            return $"TEMPERATURE: TIMESTAMP: {TimeStamp}, TEMPERATURE: {TemperatureInDegrees}";
        }
    }
}