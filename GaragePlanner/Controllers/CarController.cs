using Core;
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
            List<string> customerEmails = _customerCollection.GetCustomerEmails();


            carViewModel.CustomerEmails = customerEmails;


            return View(carViewModel);
        }

        [HttpPost]

        public ActionResult AddCar(AddCarViewModel carViewModel)
        {
            

            Car car = new(
                carViewModel.LicensePlate,
                carViewModel.SelectedColor,
                carViewModel.Model,
                carViewModel.Year
            );
            string email = carViewModel.SelectedCustomerEmail;


            Result result = _carCollection.CreateCar(email,car);


            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteCar(int id)
        {
            _carCollection.DeleteCar(id);
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

            _carCollection.EditCar(car);


            return RedirectToAction("Overview");
        }

        public ActionResult Overview(OverviewCarViewModel model)
        {
            model.CustomerEmails = _customerCollection.GetCustomerEmails();
            List<Car> customerCars = _carCollection.GetCustomerCarsByCustomerEmail("fefefefefefefe@gmail.com");
            model.Cars = customerCars;
            return View(model);
        }



    }
}


