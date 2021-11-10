using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Temperature
    {
        [Key]
        public string TimeStamp { get; set; }
        public double TemperatureInDegrees { get; set; }
    }
}