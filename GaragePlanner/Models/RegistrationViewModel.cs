using System.ComponentModel.DataAnnotations;

namespace GaragePlanner.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Please enter a first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter an Address.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter an Email Address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a Password.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter a Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }


        public RegistrationViewModel()
        {

        }
    }
}