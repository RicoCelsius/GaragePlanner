using Domain;

namespace GaragePlanner.Models
{
    public class DayViewModel
    {
        public DateOnly Date { get; set; }
        public List<TimeSlotViewModel> TimeSlots { get; set; }

        public DayViewModel()
        {
            TimeSlots = new List<TimeSlotViewModel>();
        }

    }
}
