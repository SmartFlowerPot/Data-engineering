using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        
        [Required, ] public string AccountName { get; set; }

        [Required] public string Password { get; set; }
    }
}
