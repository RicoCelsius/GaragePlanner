using Domain;
using Domain.dto;
using System.ComponentModel.DataAnnotations;

namespace GaragePlanner.Models
{
    public class OverviewCarViewModel
    {
        public List<string> CustomerEmails { get; set; }
        public string SelectedCustomerEmail { get; set; }
        public List<Car> Cars { get; set; }

        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a license plate number.")]
        public string LicensePlate { get; set; }
        [Required(ErrorMessage = "Please enter a color.")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Please enter a model.")]
        public string CarModel { get; set; }
        [Required(ErrorMessage = "Please enter a year.")]
        [RegularExpression("([1-9]+)", ErrorMessage = "Please enter a valid year")]
        public int Year { get; set; }

        public OverviewCarViewModel()
        {

        }
    }
}