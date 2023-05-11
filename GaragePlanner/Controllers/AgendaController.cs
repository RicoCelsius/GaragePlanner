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
            Agenda agenda = new Agenda(_appointmentDal);
            List<Timeslot> appointments = agenda.GetAgenda();

            model.Appointments = appointments;




            return View(model);
        }




        [HttpGet]
        [HttpPost]

        public IActionResult BookInformation(BookViewModel model, DateTime dateAndTime, Car selectedCar, Enums.Type selectedTypeOfAppointment, string selectedCustomerEmail)
        {
            Agenda agenda = new Agenda(_appointmentDal);
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
            Agenda agenda = new Agenda(_appointmentDal);
            CustomerCollection customerCollection = new CustomerCollection(_customerDal);
            int id = customerCollection.GetCustomerIdByEmail(model.selectedEmail);



            agenda.TryCreateAppointment(id,model.ChosenDateTime,model.SelectedTypeOfAppointment);

            // Redirect to the confirmation page with the model
            return RedirectToAction("Confirmation",model);

        }

        public IActionResult Confirmation(BookViewModel model)
        {



            return View(model);
        }

    }
}
