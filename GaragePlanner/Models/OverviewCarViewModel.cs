using Domain;
using Domain.dto;

namespace GaragePlanner.Models
{
    public class OverviewCarViewModel
    {
        public List<string> CustomerEmails { get; set; }
        public string SelectedCustomerEmail { get; set; }
        public List<Car> Cars { get; set; }

        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Color { get; set; }
        public string CarModel { get; set; }
        public int Year { get; set; }

        public OverviewCarViewModel()
        {

        }
    }
}