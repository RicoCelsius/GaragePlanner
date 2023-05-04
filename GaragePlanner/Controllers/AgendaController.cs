using Domain;
using Domain.interfaces;
using GaragePlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace GaragePlanner.Controllers
{
    public class AgendaController : Controller
    {
        private readonly IAppointmentDal _appointmentDal;

        public AgendaController(IAppointmentDal appointmentDal)
        {
            this._appointmentDal = appointmentDal;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Book(BookViewModel model)
        {
            AppointmentCollection appointmentCollection = new AppointmentCollection(_appointmentDal);
            
            
            
            return View();
        }
    }
}
