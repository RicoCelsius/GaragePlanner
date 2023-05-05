using Core;
using Domain;
using Domain.interfaces;
using GaragePlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace GaragePlanner.Controllers
{
    public class AgendaController : Controller
    {
        private readonly IAppointmentDal _appointmentDal;
        private readonly ICustomerDal _customerDal;

        public AgendaController(IAppointmentDal appointmentDal, ICustomerDal customerDal)
        {
            this._appointmentDal = appointmentDal;
            this._customerDal = customerDal;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [HttpPost]

        public IActionResult ShowAgenda(AgendaViewModel model, DateTime dateAndTime)
        {
            AppointmentCollection appointmentCollection = new AppointmentCollection(_appointmentDal);

            List<DateTime> datesAndTimes = appointmentCollection.GenerateDatesAndTimeSlots();
            List<DateTime> availableDatesAndTimeSlots = appointmentCollection.GetAvailableDateAndTimeSlots();

            model.AvailableDatesAndTimeSlots = availableDatesAndTimeSlots;
            model.Dates = datesAndTimes;
            model.TimeSlots = datesAndTimes;


            return View(model);
        }

        public IActionResult Book(BookViewModel model, DateTime dateAndTime)
        {
            AppointmentCollection appointmentCollection = new AppointmentCollection(_appointmentDal);
            CustomerCollection customerCollection = new CustomerCollection(_customerDal);
            List<String> customers = customerCollection.GetCustomerNames();
            model.CustomerNames = customers;
            model.ChosenDateTime = dateAndTime;

            appointmentCollection.TryCreateAppointment(dateAndTime);




            return View(model);
        }

    }
}
