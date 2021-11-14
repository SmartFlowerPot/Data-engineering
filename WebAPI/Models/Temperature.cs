using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Temperature
    {
        //TODO Class was modified for testing purposes!!!
        [Key]
        public string TimeStamp { get; set; }
        public double TemperatureInDegrees { get; set; }

        public string Data { get; set; }
        public string EUI { get; set; }
    }
}