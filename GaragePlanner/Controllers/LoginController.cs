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


        public LoginController(CustomerDal customerDal)
        {
         
            _customerCollection = new CustomerCollection(customerDal);
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
            Result authenticatedCustomer = _customerCollection.AuthenticateCustomer(model.Email,model.Password);

           



            return View("dashboard",authenticatedCustomer);
        }
    }
}
