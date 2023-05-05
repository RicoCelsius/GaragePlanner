using Domain;

namespace GaragePlanner.Models;

public class BookViewModel
{



    public DateTime ChosenDateTime { get; set; }
    public List<string> CustomerNames { get; set; }
    public BookViewModel()
    {
    }

}