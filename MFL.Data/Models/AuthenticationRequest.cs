using System.ComponentModel.DataAnnotations;

namespace MFL.Data.Models
{
    public class AuthenticationRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
