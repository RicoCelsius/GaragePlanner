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
        private ICarDal _carDal;
        private ICustomerDal _customerDal;

        public CarController(ICarDal carDal, ICustomerDal customerDal)
        {
            _carDal = carDal;
            _customerDal = customerDal;
        }

        [HttpGet]
        public ActionResult Index(AddCarViewModel carViewModel)
        {
            List<string> customerEmails = new List<string>();
            CustomerCollection customerCollection = new CustomerCollection(_customerDal);
            customerEmails = customerCollection.GetCustomerEmails();
            carViewModel.CustomerEmails = customerEmails;


            return View(carViewModel);
        }

        [HttpPost]

        public ActionResult AddCar(AddCarViewModel carViewModel)
        {
            CarCollection carCollection = new CarCollection(_carDal);
            

            Car car = new Car(
                carViewModel.LicensePlate,
                carViewModel.SelectedColor,
                carViewModel.Model,
                carViewModel.Year
            );
            string email = carViewModel.SelectedCustomerEmail;


            Result result = carCollection.CreateCar(email,car);
            if (result.Success == false)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteCar(int id)
        {
            CarCollection carCollection = new CarCollection(_carDal);
            carCollection.DeleteCar(id);


            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditCar(OverviewCarViewModel model)
        {
            Car car = new Car(
                model.Id,
                model.LicensePlate,
                model.Color,
                model.CarModel,
                model.Year
                );

            CarCollection carCollection = new CarCollection(_carDal);
            carCollection.EditCar(car);


            return RedirectToAction("Overview");
        }

        public ActionResult Overview(OverviewCarViewModel model)
        {
            CustomerCollection customerCollection = new CustomerCollection(_customerDal);
            CarCollection carCollection = new CarCollection(_carDal);

            model.CustomerEmails = customerCollection.GetCustomerEmails();
            List<Car> customerCars = carCollection.GetCustomerCarsByCustomerEmail("fefefefefefefe@gmail.com");
            model.Cars = customerCars;
            return View(model);
        }



    }
}


