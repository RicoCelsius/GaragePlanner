﻿using Core;
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
        public ReservationController(IAppointmentDal appointmentDal, ICustomerDal customerDal, ICarDal carDal)
        {
            _customerDal = customerDal;
            _appointmentDal = appointmentDal;
            _carDal = carDal;
        }


        [HttpGet]
        [HttpPost]

        public IActionResult BookInformation(BookViewModel model, string selectedCustomerEmail, DateTime dateAndTime)
        {
            CustomerCollection customerCollection = new(_customerDal);
            CarCollection carCollection = new(_carDal);
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
            CarCollection carCollection = new(_carDal);
            Customer customer = customerCollection.GetCustomerByEmail(model.SelectedEmail);
            Car car = carCollection.GetCarById(model.SelectedCarId);

            Appointment appointment = new(model.ChosenDateTime, model.SelectedTypeOfAppointment, Enums.Status.Scheduled,
                customer, car);
            agenda.CreateAppointment(appointment);

            return RedirectToAction("Confirmation", model);

        }

        public IActionResult Confirmation(BookViewModel model)
        {

            return View(model);
        }
    }
}
