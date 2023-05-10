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
        public IActionResult Index(DateTime dataAndTime)
        {
            Agenda agenda = new(_appointmentDal);
            AgendaViewModel model = new();
            DateTime appointmentDateTime = DateTime.Today + new TimeSpan(14, 0, 0); // 2:00 PM today
            agenda.TryCreateAppointment(appointmentDateTime, Enums.Type.BigMaintenance,Enums.Status.Cancelled, new Customer("rico", "a", "d", "w", "d"),new Car());

            /*List <Day> days = agenda.Days;
            

            foreach (var day in days)
            {
                DayViewModel dayViewModel = new DayViewModel { DateOfDay = day.DateOfDay };
                foreach (TimeSlot timeslot in day.TimeSlots)
                {
                    TimeSlotViewModel timeslotViewModel = new TimeSlotViewModel
                    {
                        Time = timeslot.StartTime,
                        IsAvailable = timeslot.HasAppointment(),
                    };
                    dayViewModel.TimeSlots.Add(timeslotViewModel);
                }
                model.Days.Add(dayViewModel);
            }*/

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
            Agenda agenda = new(_appointmentDal);
            CustomerCollection customerCollection = new CustomerCollection(_customerDal);
            /*int id = customerCollection.GetCustomerIdByEmail(model.selectedEmail);

            appointmentCollection.TryCreateAppointment(id,model.ChosenDateTime,model.SelectedTypeOfAppointment);*/

            agenda.TryCreateAppointment(model.ChosenDateTime, model.SelectedTypeOfAppointment, Enums.Status.Scheduled, customerCollection.GetCustomerByEmail(model.selectedEmail), model.SelectedCar);

            // Redirect to the confirmation page with the model
            return RedirectToAction("Confirmation",model);

        }

        public IActionResult Confirmation(BookViewModel model)
        {

            return View(model);
        }

    }
}
