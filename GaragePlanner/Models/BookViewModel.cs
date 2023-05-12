using Domain;
using Domain.dto;

namespace GaragePlanner.Models;

public class BookViewModel
{



    public DateTime ChosenDateTime { get; set; }
    public List<string> CustomerEmails { get; set; }
    
    public List<CarDto> CustomerCars { get; set; }
    public Car SelectedCar { get; set; }

    public string SelectedEmail { get; set; }
    
    public Enums.Type SelectedTypeOfAppointment { get; set; }

    public BookViewModel()
    {
    }

}