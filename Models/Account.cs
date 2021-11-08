using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Account
    {
        [Required, Key] public string AccountName { get; set; }

        [Required] public string Password { get; set; }
    }
}
