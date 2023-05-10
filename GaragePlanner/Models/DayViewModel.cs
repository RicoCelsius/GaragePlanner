using Domain;

namespace GaragePlanner.Models
{
    public class DayViewModel
    {
        public DateOnly Date { get; set; }
        public List<TimeslotViewModel> TimeSlots { get; set; }

    }
}
