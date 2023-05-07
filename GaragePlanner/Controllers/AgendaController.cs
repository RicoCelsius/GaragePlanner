using Core;
using Domain;
using Domain.interfaces;
using GaragePlanner.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

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
        public IActionResult Index(AgendaViewModel model, DateTime dataAndTime)
        {
            AppointmentCollection appointmentCollection = new AppointmentCollection(_appointmentDal);

            List<DateTime> datesAndTimes = appointmentCollection.GenerateDatesAndTimeSlots();
            List<DateTime> availableDatesAndTimeSlots = appointmentCollection.GetAvailableDateAndTimeSlots();

            model.AvailableDatesAndTimeSlots = availableDatesAndTimeSlots;
            model.Dates = datesAndTimes;
            model.TimeSlots = datesAndTimes;


            return View(model);
        }




        [HttpGet]
        [HttpPost]

        public IActionResult BookInformation(BookViewModel model, DateTime dateAndTime, Car selectedCar, Enums.Type selectedTypeOfAppointment, string selectedCustomerEmail)
        {
            AppointmentCollection appointmentCollection = new AppointmentCollection(_appointmentDal);
            CustomerCollection customerCollection = new CustomerCollection(_customerDal);
            List<String> customerEmails = customerCollection.GetCustomerEmails();
            model.CustomerEmails = customerEmails;
            model.ChosenDateTime = dateAndTime;
            model.SelectedTypeOfAppointment = selectedTypeOfAppointment;
            model.selectedEmail = selectedCustomerEmail;






            return View(model);
        }


        [HttpPost]
        public IActionResult Book(BookViewModel model)
        {


            // Create a new appointment using the chosen date and time
            AppointmentCollection appointmentCollection = new AppointmentCollection(_appointmentDal);
            CustomerCollection customerCollection = new CustomerCollection(_customerDal);
            int id = customerCollection.GetCustomerIdByEmail(model.selectedEmail);



            appointmentCollection.TryCreateAppointment(id,model.ChosenDateTime,model.SelectedTypeOfAppointment);

            // Redirect to the confirmation page with the model
            return RedirectToAction("Confirmation",model);

        }

        public IActionResult Confirmation(BookViewModel model)
        {



            return View(model);
        }

    }
}
