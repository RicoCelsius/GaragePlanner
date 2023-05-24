using Core;
using Domain;
using Domain.interfaces;
using GaragePlanner.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using DAL;
using Domain.dto;
using Domain.utils;

namespace GaragePlanner.Controllers
{
    public class AgendaController : Controller
    {
        private readonly IAppointmentDal _appointmentDal;
        private readonly ICustomerDal _customerDal;
        private readonly ICarDal _carDal;

        public AgendaController(IAppointmentDal appointmentDal, ICustomerDal customerDal, ICarDal carDal)
        {
            _appointmentDal = appointmentDal;
            _customerDal = customerDal;
            _carDal = carDal;
        }
        public IActionResult Index(DateTime dataAndTime)
        {
            Agenda agenda = new Agenda(_appointmentDal);
            AgendaViewModel model = new();

            List <Day> days = agenda.Days;
            

            foreach (var day in days)
            {
                DayViewModel dayViewModel = new DayViewModel { Date = day.DateOfDay };
                foreach (TimeSlot timeslot in day.TimeSlots)
                {
                    TimeSlotViewModel timeslotViewModel = new TimeSlotViewModel
                    {
                        Time = timeslot.StartTime,
                        IsAvailable = timeslot.IsAvailable(),
                    };
                    dayViewModel.TimeSlots.Add(timeslotViewModel);
                }
                model.Days.Add(dayViewModel);
            }

            return View(model);




        }


        [HttpGet]
        [HttpPost]

        public IActionResult BookInformation(BookViewModel model, DateTime dateAndTime, Enums.Type selectedTypeOfAppointment, string selectedCustomerEmail)
        {
            CustomerCollection customerCollection = new CustomerCollection(_customerDal);
            CarCollection carCollection = new CarCollection(_carDal);
            List<String> customerEmails = customerCollection.GetCustomerEmails();

            List<Car> customerCars = carCollection.GetCustomerCarsByCustomerEmail("fefefefefefefe@gmail.com");

            model.CustomerEmails = customerEmails;
            model.ChosenDateTime = dateAndTime;
            model.SelectedTypeOfAppointment = selectedTypeOfAppointment;
            model.SelectedEmail = selectedCustomerEmail;
            model.CustomerCars = customerCars;

            return View(model);
        }



        [HttpPost]
        public IActionResult Book(BookViewModel model)
        {


            Agenda agenda = new(_appointmentDal);
            CustomerCollection customerCollection = new CustomerCollection(_customerDal);
            Customer customer = customerCollection.GetCustomerByEmail(model.SelectedEmail);

            Appointment appointment = new(model.ChosenDateTime, model.SelectedTypeOfAppointment, Enums.Status.Scheduled,
                customer, new Car(10,"995295", "s", "d", 1990));

            //995295 is the string that is used to create a car in the database. Needs to exist in the db so has to be taken from the cars a customer has.


            agenda.AddAppointment(appointment);
            



            return RedirectToAction("Confirmation",model);

        }

        public IActionResult Confirmation(BookViewModel model)
        {

            return View(model);
        }

    }
}
