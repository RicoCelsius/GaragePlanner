using System.ComponentModel.DataAnnotations;
using Domain;

namespace GaragePlanner.Models
{
    public class AddCarViewModel
    {
        public List<string> CustomerEmails { get; set; }
        public string SelectedCustomerEmail { get; set; }

        [Required(ErrorMessage = "Please enter a license plate number.")]
        public string LicensePlate { get; set; }
        [Required(ErrorMessage = "Please enter a color.")]
        public Enums.Color SelectedColor { get; set; }
        [Required(ErrorMessage = "Please enter a model.")]
        public List<string> Brands { get; set; }
        public string SelectedBrand { get; set; }
        [Required(ErrorMessage = "Please enter a year.")]
        [RegularExpression("([1-9]+)", ErrorMessage = "Please enter a valid year")]
        public int Year { get; set; }


    }
}
