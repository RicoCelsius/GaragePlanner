using Domain;

namespace GaragePlanner.Models;

public class AgendaViewModel
{



    public List<DayViewModel> Days { get; set; } 



    public AgendaViewModel()
    {
        Days = new List<DayViewModel>();
    }

}