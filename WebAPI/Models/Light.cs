using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Light
    {
        [Key]
        public int Id { get; set; }
    
        public decimal LightLevel { get; set; }
        public DateTime TimeStamp { get; set; }
        
        public string EUI { get; set; }

        public override string ToString()
        {
            return $"Light: TIMESTAMP: {TimeStamp}, Light level: {LightLevel}, ID: {Id},EUI: {EUI}";
        }
    }
}