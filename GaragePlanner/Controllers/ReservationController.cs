using Core;
using DAL;
using Domain;
using Domain.interfaces;
using Domain.utils;
using GaragePlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace GaragePlanner.Controllers
{
    public class ReservationController : Controller
    {
        private readonly AppointmentCollection _appointmentCollection;
        private readonly CustomerCollection _customerCollection;
        private readonly CarCollection _carCollection;
  
        public ReservationController(AppointmentCollection appointmentCollection, CustomerCollection customerCollection, CarCollection carCollection)
        {
            _appointmentCollection = appointmentCollection;
            _customerCollection = customerCollection;
            _carCollection = carCollection;
         
        }


        [HttpGet]
        [HttpPost]

        public IActionResult BookInformation(BookViewModel model, string selectedCustomerEmail, DateTime dateAndTime, string chosenDate, string chosenTime)
        {
            try
            {
                List<string> customerEmails = _customerCollection.GetCustomerEmails();
                if (!string.IsNullOrEmpty(selectedCustomerEmail))
                {
                    List<Car> customerCars = _carCollection.GetCustomerCarsByCustomerEmail(selectedCustomerEmail);
                    model.CustomerCars = customerCars;
                }

                model.CustomerEmails = customerEmails;
                model.SelectedEmail = selectedCustomerEmail;

                model.ChosenDate = !string.IsNullOrEmpty(chosenDate)
                    ? chosenDate
                    : dateAndTime.Date.ToString("yyyy-MM-dd");
                model.ChosenTime = !string.IsNullOrEmpty(chosenTime)
                    ? chosenTime
                    : dateAndTime.TimeOfDay.ToString("hh\\:mm");
            }
            catch (CouldNotReadDataException)
            {
                ErrorViewModel errorViewModel = new()
                {
                    ErrorMessage = "Something went wrong, please try again"
                };
                return View("Error", errorViewModel);
            }

            return View(model);
        }






        [HttpPost]
        public IActionResult Book(BookViewModel model)
        {
            try
            {


                Customer customer = _customerCollection.GetCustomerByEmail(model.SelectedEmail);
                Car car = _carCollection.GetCarById(model.SelectedCarId);
                _appointmentCollection.TryCreateAppointment(DateOnly.Parse(model.ChosenDate),
                    TimeOnly.Parse(model.ChosenTime), model.SelectedTypeOfAppointment, customer, car);
            }
            catch (CouldNotReadDataException)
            {

            }

            return RedirectToAction("Confirmation", model);

        }

        public IActionResult Confirmation(BookViewModel model)
        {
            return View(model);
        }
    }
}
