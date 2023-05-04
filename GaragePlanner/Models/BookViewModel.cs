namespace GaragePlanner.Models;

public class BookViewModel
{
    public List<DateTime>? AvailableDates { get; set; }


    public DateTime? SelectedDate { get; set; }
    public List<String> AvailableTimeSlots { get; set; }

    public string SelectedTimeSlot { get; set; }

    public BookViewModel()
    {
    }

}