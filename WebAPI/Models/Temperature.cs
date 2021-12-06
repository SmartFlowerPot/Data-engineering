using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebAPI.Models
{
    public class Temperature
    {
        //TODO Class was modified for testing purposes!!!
        
        [Key]
        public int Id { get; set; }
        
        public DateTime TimeStamp { get; set; }

        public long Ts { get; set; }
        
        [Column("Temperature")]
        public decimal TemperatureInDegrees { get; set; }
        
        public string Data { get; set; }
        
        public string EUI { get; set; }

        public override string ToString()
        {
            return $"TEMPERATURE: TIMESTAMP: {TimeStamp}, TEMPERATURE: {TemperatureInDegrees}";
        }
    }
}