using System.ComponentModel.DataAnnotations;

namespace GaragePlanner.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a password.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        public LoginViewModel()
        {
        }   
    }
}
