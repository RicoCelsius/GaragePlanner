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
            try
            {
                Agenda agenda = new(_appointmentDal);
                AgendaViewModel model = new();

                List<Day> days = agenda.Days;


                foreach (var day in days)
                {
                    DayViewModel dayViewModel = new() { Date = day.DateOfDay };
                    foreach (TimeSlot timeslot in day.TimeSlots)
                    {
                        TimeSlotViewModel timeslotViewModel = new()
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
            catch (CouldNotReadDataException)
            {
                var errorViewModel = new ErrorViewModel()
                {
                    ErrorMessage = "Technical issues, please try again later."
                };

                return View("Error", errorViewModel);
            }
        }


        [HttpGet]
        [HttpPost]

        public IActionResult BookInformation(BookViewModel model, string selectedCustomerEmail, DateTime dateAndTime)
        {
            CustomerCollection customerCollection = new(_customerDal);
            CarCollection carCollection = new (_carDal);
            List<String> customerEmails = customerCollection.GetCustomerEmails();

            if (!string.IsNullOrEmpty(selectedCustomerEmail))
            {
                List<Car> customerCars = carCollection.GetCustomerCarsByCustomerEmail(selectedCustomerEmail);
                model.CustomerCars = customerCars;
            }

            model.CustomerEmails = customerEmails;
            model.SelectedEmail = selectedCustomerEmail;
            model.ChosenDateTime = dateAndTime;

            return View(model);
        }





        [HttpPost]
        public IActionResult Book(BookViewModel model)
        {


            Agenda agenda = new(_appointmentDal);
            CustomerCollection customerCollection = new(_customerDal);
            CarCollection carCollection = new (_carDal);
            Customer customer = customerCollection.GetCustomerByEmail(model.SelectedEmail);
            Car car = carCollection.GetCarById(model.SelectedCarId);

            Appointment appointment = new(model.ChosenDateTime, model.SelectedTypeOfAppointment, Enums.Status.Scheduled,
                customer, car);
            agenda.CreateAppointment(appointment);
            
            return RedirectToAction("Confirmation",model);

        }

        public IActionResult Confirmation(BookViewModel model)
        {

            return View(model);
        }

    }
}
