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
        public ActionResult Create(CarViewModel carViewModel, [FromForm] string customerName)
        {
            List<string> customerNames = new List<string>();
            CustomerCollection customerCollection = new CustomerCollection(_customerDal);
            customerNames = customerCollection.GetCustomerNames();
            carViewModel.CustomerNames = customerNames;
            if (!ModelState.IsValid)
            {
                return View(carViewModel);
            }

            Car car = new Car
            {
                CustomerName = customerName,
                LicensePlate = carViewModel.LicensePlate,
                Color = carViewModel.Color,
                Model = carViewModel.Model,
                Year = carViewModel.Year

            };


            CarCollection carCollection = new CarCollection(_carDal);
            carCollection.CreateCar(car);


            return View(carViewModel);
        }
    }
}


