using Domain;
using Domain.dto;

namespace GaragePlanner.Models
{
    public class OverviewCarViewModel
    {
        public List<string> CustomerEmails { get; set; }
        public string SelectedCustomerEmail { get; set; }

        public List<Car> Cars { get; set; }
        int CarId { get; set; }
        public OverviewCarViewModel()
        {
            CustomerEmails = new List<string>();
            Cars = new List<Car>();
        }
    }
}