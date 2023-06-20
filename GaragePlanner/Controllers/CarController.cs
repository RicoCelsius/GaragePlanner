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


        public CarController(CustomerCollection customerCollection, CarCollection carCollection)
        {
            _carCollection = carCollection;
            _customerCollection = customerCollection;

        }

        [HttpGet]
        public ActionResult Index(AddCarViewModel carViewModel)
        {
            try
            {
                carViewModel.Brands = _carCollection.GetAllCurrentBrands();
                


                carViewModel.CustomerEmails = _customerCollection.GetCustomerEmails();
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

        public ActionResult AddCar(AddCarViewModel addCarViewModel)
        {

            try
            {
                string email = addCarViewModel.SelectedCustomerEmail;
                string brand = addCarViewModel.SelectedBrand;
                if (!_carCollection.TryCreateCar(email, addCarViewModel.LicensePlate, brand,
                        addCarViewModel.SelectedColor, addCarViewModel.Year))
                {
                    ErrorViewModel errorViewModel = new()
                    {
                        ErrorMessage = "License plate already exist. Contact support if this should not be the case."
                    };
                    return View("Error", errorViewModel);
                };
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
            model.Brands = _carCollection.GetAllCurrentBrands();
            Car car = new (
                model.Id,
                model.LicensePlate,
                model.Color,
                model.SelectedBrand,
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
                model.Brands = _carCollection.GetAllCurrentBrands();
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


