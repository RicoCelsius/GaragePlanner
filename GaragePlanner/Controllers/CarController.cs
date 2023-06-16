﻿using Core;
using DAL;
using Domain;
using Domain.dto;
using Domain.interfaces;
using GaragePlanner.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Domain.utils;

namespace GaragePlanner.Controllers
{
    public class CarController : Controller
    {

        private readonly CustomerCollection _customerCollection;
        private readonly CarCollection _carCollection;
        private readonly ICustomerDal _customerDal;
        private readonly ICarDal _carDal;

        public CarController(CustomerDal customerDal, CarDal carDal)
        {
            _customerDal = customerDal;
            _carDal = carDal;
            _customerCollection = new(_customerDal); 
            _carCollection = new(_carDal);
        }

        [HttpGet]
        public ActionResult Index(AddCarViewModel carViewModel)
        {
            try
            {
                List<string> customerEmails = _customerCollection.GetCustomerEmails();


                carViewModel.CustomerEmails = customerEmails;
            }
            catch (DalException)
            {
                ErrorViewModel errorViewModel = new()
                {
                    ErrorMessage = "Something went wrong, please try again"
                };
                return View("Error", errorViewModel);
            }


            return View(carViewModel);
        }

        [HttpPost]

        public ActionResult AddCar(AddCarViewModel carViewModel)
        {

            try
            {
                string email = carViewModel.SelectedCustomerEmail;
                Result result = _carCollection.TryCreateCar(email, carViewModel.LicensePlate, carViewModel.Model,
                    carViewModel.SelectedColor, carViewModel.Year);
            }
            catch(DalException)
            {
                ErrorViewModel errorViewModel = new()
                {
                    ErrorMessage = "Something went wrong, please try again"
                };
                return View("Error", errorViewModel);
            }


            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteCar(int id)
        {
            try
            {
                _carCollection.DeleteCar(id);

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

            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditCar(OverviewCarViewModel model)
        {
            Car car = new (
                model.Id,
                model.LicensePlate,
                model.Color,
                model.CarModel,
                model.Year
                );
            try
            {
                _carCollection.EditCar(car);
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


            return RedirectToAction("Overview");
        }

        public ActionResult Overview(OverviewCarViewModel model)
        {
            try
            {
                model.CustomerEmails = _customerCollection.GetCustomerEmails();
                List<Car> customerCars = _carCollection.GetCustomerCarsByCustomerEmail("fefefefefefefe@gmail.com");
                model.Cars = customerCars;
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



    }
}


