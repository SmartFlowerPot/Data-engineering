using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Account
    {
        [Required, Key]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        
        public IList<Plant> Plants { get; set; }

        public DateTime DateOfBirth { get; set; }
        
        [StringLength(50)]
        public string Gender { get; set; }
        
        [StringLength(50)]
        public string Region { get; set; }
    }
}
