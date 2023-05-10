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

        public ActionResult Index()
        {
            return View();
        }

        // GET: CarController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }



        // GET: CarController/Create
        public ActionResult Create(CarViewModel carViewModel, [FromForm] string customerEmail)
        {
            List<string> customerEmails = new List<string>();
            CustomerCollection customerCollection = new CustomerCollection(_customerDal);
            customerEmails = customerCollection.GetCustomerEmails();
            carViewModel.CustomerEmails = customerEmails;
            if (!ModelState.IsValid)
            {
                return View(carViewModel);
            }

            Car car = new Car(
                carViewModel.LicensePlate,
                carViewModel.Color,
                carViewModel.Model,
                carViewModel.Year
            );


            CarCollection carCollection = new CarCollection(_carDal);
            carCollection.CreateCar(car);


            return View(carViewModel);
        }
    }
}


