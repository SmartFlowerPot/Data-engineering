using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Account
    {
        [Required, Key] 
        public string Username { get; set; }

        [Required] 
        public string Password { get; set; }

        public IList<Plant> Plants { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string Region { get; set; }
    }
}
