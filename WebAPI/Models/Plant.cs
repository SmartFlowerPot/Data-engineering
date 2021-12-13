using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class Plant
    {
        public DateTime DOB { get; set; }

        [StringLength(50)]
        public string Nickname { get; set; }
        
        [Key]
        [StringLength(50)]
        public string EUI { get; set; }
        
        [NotMapped] 
        public int Age { get; set; }
        
        [StringLength(50)]
        public string PlantType { get; set; }

        [JsonIgnore]
        public IList<Measurement> Measurements { get; set; }

        public void SetAge()
        {
            DateTime now = DateTime.Now;
            var ageInDays = now.Subtract(DOB).Days;
            int age = ageInDays / 365;
            Age = age;
        }
    }
}