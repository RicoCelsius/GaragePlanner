﻿using Core;
using DAL;
using Domain;
using Domain.interfaces;
using Domain.utils;
using GaragePlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace GaragePlanner.Controllers
{
    public class LoginController : Controller
    {
        private readonly CustomerCollection _customerCollection;


        public LoginController(CustomerCollection customerCollection)
        {
            _customerCollection = customerCollection;
        }
        
    

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            bool authenticatedCustomer = _customerCollection.AuthenticateCustomer(model.Email,model.Password);
            if (!authenticatedCustomer)
            {
                ErrorViewModel errorViewModel = new()
                {
                    ErrorMessage = "Wrong email or password"
                };
                return View("Error", errorViewModel);
                
            }


            return View("dashboard", authenticatedCustomer);



        }
    }
}
