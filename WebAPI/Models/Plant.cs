using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Plant
    {
        public int Id { get; set; }
        public DateTime DOB { get; set; }
        public string Nickname { get; set; }
        public string EUI { get; set; }
        [NotMapped] 
        public int Age { get; set; }

        public void SetAge()
        {
            DateTime now = DateTime.Now;
            var ageInDays = now.Subtract(DOB).Days;
            int age = ageInDays / 365;
            Age = age;
        }
    }
}