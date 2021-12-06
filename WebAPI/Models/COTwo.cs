using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class COTwo
    {
        [Key]
        public int Id { get; set; }
        public double CO2Level { get; set; }
        public DateTime TimeStamp { get; set; }
        
        public string EUI { get; set; }

        public override string ToString()
        {
            return $"CO2: TIMESTAMP: {TimeStamp}, CO2 level: {CO2Level}";
        }
    }
}