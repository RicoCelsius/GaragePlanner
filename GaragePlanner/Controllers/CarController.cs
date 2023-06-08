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
        private readonly ICarDal _carDal;
        private readonly ICustomerDal _customerDal;

        public CarController(ICarDal carDal, ICustomerDal customerDal)
        {
            _carDal = carDal;
            _customerDal = customerDal;
        }

        [HttpGet]
        public ActionResult Index(AddCarViewModel carViewModel)
        {
            List<string> customerEmails = new ();
            CustomerCollection customerCollection = new (_customerDal);
            customerEmails = customerCollection.GetCustomerEmails();
            carViewModel.CustomerEmails = customerEmails;


            return View(carViewModel);
        }

        [HttpPost]

        public async Task<ActionResult> AddCarAsync(AddCarViewModel carViewModel)
        {
            CarCollection carCollection = new (_carDal);
            

            Car car = new(
                carViewModel.LicensePlate,
                carViewModel.SelectedColor,
                carViewModel.Model,
                carViewModel.Year
            );
            string email = carViewModel.SelectedCustomerEmail;


            Result result = await carCollection.CreateCarAsync(email,car);


            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteCar(int id)
        {
            CarCollection carCollection = new (_carDal);
            carCollection.DeleteCar(id);


            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditCar(OverviewCarViewModel model)
        {
            Car car = new (
                model.Id,
                model.LicensePlate,
                model.Color,
                model.Model,
                model.Year
                );

            CarCollection carCollection = new (_carDal);
            carCollection.EditCar(car);


            return RedirectToAction("Overview");
        }

        public async Task<ActionResult> OverviewAsync(OverviewCarViewModel model)
        {
            CustomerCollection customerCollection = new (_customerDal);
            CarCollection carCollection = new (_carDal);

            model.CustomerEmails = customerCollection.GetCustomerEmails();
            List<Car> customerCars = await carCollection.GetCustomerCarsByCustomerEmailAsync("fefefefefefefe@gmail.com");
            model.Cars = customerCars;
            return View(model);
        }



    }
}


