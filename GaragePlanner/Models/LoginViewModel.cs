using System.ComponentModel.DataAnnotations;

namespace GaragePlanner.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter an Email Address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email Address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a Password.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

    }
}
