using Domain;

namespace GaragePlanner.Models;

public class AgendaViewModel
{


    public List<DateTime> AvailableDatesAndTimeSlots { get; set; }
    public List<DateTime> Dates { get; set; }
    public List<DateTime> TimeSlots { get; set; }


    public AgendaViewModel()
    {
    }

}