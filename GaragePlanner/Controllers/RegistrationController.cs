using Core;
using DAL;
using Domain.interfaces;
using Domain.utils;
using GaragePlanner.Models;
using Microsoft.AspNetCore.Mvc;




namespace GaragePlanner.Controllers

{
    public class RegistrationController : Controller
    {
        private readonly CustomerCollection _customerCollection;
        public RegistrationController(CustomerCollection customerCollection )
        {
            _customerCollection = customerCollection;
        }   

        public IActionResult Index()
        {
            return View();
        }

 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterCustomer(RegistrationViewModel registrationViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registrationViewModel);
            }

            try
            {

                _customerCollection.TryCreateCustomer(
                    registrationViewModel.FirstName,
                    registrationViewModel.LastName,
                    registrationViewModel.Address,
                    registrationViewModel.Email,
                    registrationViewModel.Password
                );
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

            return RedirectToAction("RegistrationSuccess", registrationViewModel);
        }

        public IActionResult RegistrationSuccess(RegistrationViewModel model)
        {
            return View(model);
        }
    }

}
