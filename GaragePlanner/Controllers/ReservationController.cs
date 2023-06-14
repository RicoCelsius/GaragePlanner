using Core;
using DAL;
using Domain;
using Domain.interfaces;
using GaragePlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace GaragePlanner.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IAppointmentDal _appointmentDal;
        private readonly ICustomerDal _customerDal;
        private readonly ICarDal _carDal;
        public ReservationController(AppointmentDal appointmentDal, CustomerDal customerDal, CarDal carDal)
        {
            _customerDal = customerDal;
            _appointmentDal = appointmentDal;
            _carDal = carDal;
        }


        [HttpGet]
        [HttpPost]

        public IActionResult BookInformation(BookViewModel model, string selectedCustomerEmail, DateTime dateAndTime, string chosenDate, string chosenTime)
        {
            CustomerCollection customerCollection = new(_customerDal);
            CarCollection carCollection = new(_carDal);
            List<string> customerEmails = customerCollection.GetCustomerEmails();

            if (!string.IsNullOrEmpty(selectedCustomerEmail))
            {
                List<Car> customerCars = carCollection.GetCustomerCarsByCustomerEmail(selectedCustomerEmail);
                model.CustomerCars = customerCars;
            }

            model.CustomerEmails = customerEmails;
            model.SelectedEmail = selectedCustomerEmail;

            if (!string.IsNullOrEmpty(chosenDate))
            {
                model.ChosenDate = chosenDate;
            }
            else
            {
                model.ChosenDate = dateAndTime.Date.ToString("yyyy-MM-dd");
            }

            if (!string.IsNullOrEmpty(chosenTime))
            {
                model.ChosenTime = chosenTime;
            }
            else
            {
                model.ChosenTime = dateAndTime.TimeOfDay.ToString("hh\\:mm");
            }

            return View(model);
        }






        [HttpPost]
        public IActionResult Book(BookViewModel model)
        {


            AppointmentCollection appointmentCollection = new(_appointmentDal);
            CustomerCollection customerCollection = new(_customerDal);
            CarCollection carCollection = new(_carDal);
            Customer customer = customerCollection.GetCustomerByEmail(model.SelectedEmail);
            Car car = carCollection.GetCarById(model.SelectedCarId);


            Appointment appointment = new(DateOnly.Parse(model.ChosenDate),TimeOnly.Parse(model.ChosenTime),model.SelectedTypeOfAppointment, Enums.Status.Scheduled,
                customer, car);
            appointmentCollection.CreateAppointment(appointment);

            return RedirectToAction("Confirmation", model);

        }

        public IActionResult Confirmation(BookViewModel model)
        {

            return View(model);
        }
    }
}
