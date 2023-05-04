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

        [HttpGet]
        [HttpPost]

        public IActionResult Book(BookViewModel model, DateTime selectedDate, DateTime selectedTimeSlot)
        {
            AppointmentCollection appointmentCollection = new AppointmentCollection(_appointmentDal);
            List<DateTime> availableDates = appointmentCollection.GetAvailableDates();
            model.AvailableTimeSlots = appointmentCollection.GetAvailableTimeSlots(selectedDate);
            model.AvailableDates = availableDates;

            // If a date has been selected, get the available time slots for that date

                
            

            // If the form has been submitted and a date and time slot have been selected, create the appointment
            if (model.SelectedDate != default(DateTime) && model.SelectedTimeSlot != null)
            {
                appointmentCollection.TryCreateAppointment((DateTime)model.SelectedDate);
            }

            return View(model);
        }

    }
}
