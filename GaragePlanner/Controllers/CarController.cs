using Core;
using DAL;
using Domain;
using Domain.interfaces;
using GaragePlanner.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult Index(CarViewModel carViewModel)
        {
            List<string> customerEmails = new List<string>();
            CustomerCollection customerCollection = new CustomerCollection(_customerDal);
            customerEmails = customerCollection.GetCustomerEmails();
            carViewModel.CustomerEmails = customerEmails;


            return View(carViewModel);
        }

        [HttpPost]

        public ActionResult AddCar(CarViewModel carViewModel)
        {
            Car car = new Car(
                carViewModel.LicensePlate,
                carViewModel.Color,
                carViewModel.Model,
                carViewModel.Year
            );


            CarCollection carCollection = new CarCollection(_carDal);
            carCollection.CreateCar(car);

            return View("Index",carViewModel);
        }


    }
}


