using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Account
    {
        [Required, Key] 
        public string Username { get; set; }

        [Required] 
        public string Password { get; set; }

        public IList<Plant> Plants { get; set; }
    }
}
