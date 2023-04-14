﻿using Core;
using GaragePlanner.Models;
using Microsoft.AspNetCore.Mvc;




namespace GaragePlanner.Controllers
    
{
    public class RegistrationController : Controller
    {
        private readonly CustomerCollection _customerCollection;

        public RegistrationController()
        {
            _customerCollection = new CustomerCollection();
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
                _customerCollection.CreateCustomer(
                    registrationViewModel.FirstName,
                    registrationViewModel.LastName,
                    registrationViewModel.Address,
                    registrationViewModel.Email,
                    registrationViewModel.Password
                );
            }

            catch 
            {
                var errorViewModel = new ErrorViewModel()
                {
                    ErrorMessage = "Something unexpected happened. Please try again later."
                };
               
                return View("Error",errorViewModel);
            }

            return RedirectToAction("RegistrationSuccess", registrationViewModel);
        }

        public IActionResult RegistrationSuccess(RegistrationViewModel model)
        {
            return View(model);
        }
    }

}
