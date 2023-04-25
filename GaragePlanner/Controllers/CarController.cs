using DAL;
using Domain;
using GaragePlanner.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GaragePlanner.Controllers
{
    public class CarController : Controller
    {
        private ICarDal _carDal;

        public CarController(ICarDal carDal)
        {
            _carDal = carDal;
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



        [ValidateAntiForgeryToken]
        [HttpPost]
        // GET: CarController/Create
        public ActionResult Create(CarViewModel carViewModel)
        {
            if (ModelState.IsValid)
            {
                // Map the ViewModel to the domain model
                var car = new Car
                {
                    Id = carViewModel.Id,
                    LicensePlate = carViewModel.LicensePlate,
                    Color = carViewModel.Color,
                    Model = carViewModel.Model,
                    Year = carViewModel.Year

                };

                // Save the car to the database
                CarCollection carCollection = new CarCollection(_carDal);

                // Redirect to the list of cars for the customer
                return RedirectToAction("ViewCars", new { customerId = carViewModel.CustomerId });
            }

            // If the ModelState is not valid, redisplay the form with validation errors
            return View(carViewModel);
        }



        // GET: CarController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CarController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CarController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CarController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
