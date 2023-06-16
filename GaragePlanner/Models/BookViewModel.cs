using Domain;
using Domain.dto;

namespace GaragePlanner.Models;

public class BookViewModel
{



    public string ChosenDate { get; set; }
    public string ChosenTime { get; set; }
    public List<string> CustomerEmails { get; set; }
    
    public List<Car> CustomerCars { get; set; }
    public int SelectedCarId { get; set; }

    public string SelectedEmail { get; set; }
    
    public Enums.Type SelectedTypeOfAppointment { get; set; }

    public BookViewModel()
    {
    }

}