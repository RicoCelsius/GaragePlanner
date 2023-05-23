using System.ComponentModel.DataAnnotations;

namespace GaragePlanner.Models
{
    public class CarViewModel
    {
        public List<string> CustomerEmails { get; set; }
        public string SelectedCustomerEmail { get; set; }

        [Required(ErrorMessage = "Please enter a license plate number.")]
        public string LicensePlate { get; set; }
        [Required(ErrorMessage = "Please enter a color.")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Please enter a model.")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Please enter a year.")]
        [RegularExpression("([1-9]+)", ErrorMessage = "Please enter a valid year")]
        public int Year { get; set; }

        public CarViewModel(){
        }   
    }
}
