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
            catch (DalException exception)
            {
                Console.WriteLine(exception);
                var errorViewModel = new ErrorViewModel()
                {
                    ErrorMessage = "Technical issues, please try again later."
                };

                return View("Error", errorViewModel);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                var errorViewModel = new ErrorViewModel()
                {
                    ErrorMessage = "Something went wrong, please contact support"
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
                if (!_appointmentCollection.TryCreateAppointment(DateOnly.Parse(model.ChosenDate),
                        TimeOnly.Parse(model.ChosenTime), model.SelectedTypeOfAppointment, customer, car))
                {
                    var errorViewModel = new ErrorViewModel()
                    {
                        ErrorMessage = "This timeslot is unavailable, please select another timeslot"
                    };
                    return View("Error", errorViewModel);
                }

            }
            catch (DalException exception)
            {
                Console.WriteLine(exception);
                var errorViewModel = new ErrorViewModel()
                {
                    ErrorMessage = "Technical issues, please try again later."
                };

                return View("Error", errorViewModel);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                var errorViewModel = new ErrorViewModel()
                {
                    ErrorMessage = "Something went wrong, please contact support"
                };

                return View("Error", errorViewModel);
            }
            return RedirectToAction("Confirmation", model);

        }

        public IActionResult Confirmation(BookViewModel model)
        {
            return View(model);
        }
    }
}
