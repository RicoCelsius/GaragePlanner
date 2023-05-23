using Domain.dto;

namespace GaragePlanner.Models
{
    public class OverviewCarViewModel
    {
        public List<string> CustomerEmails { get; set; }
        public string SelectedCustomerEmail { get; set; }

        public List<CarDto> Cars { get; set; }

        public OverviewCarViewModel()
        {
            CustomerEmails = new List<string>();
            Cars = new List<CarDto>();
        }
    }
}