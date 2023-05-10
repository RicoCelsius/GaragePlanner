using Domain;

namespace GaragePlanner.Models;

public class BookViewModel
{



    public DateTime ChosenDateTime { get; set; }
    public List<string> CustomerEmails { get; set; }
    public Car SelectedCar { get; set; }
    public string selectedEmail { get; set; }
  

    public Enums.Type SelectedTypeOfAppointment { get; set; }
    public BookViewModel()
    {
    }

}