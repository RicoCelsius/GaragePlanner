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
        ICustomerDal _customerDal;


        public LoginController(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }
        
    

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            CustomerCollection customerCollection = new CustomerCollection(_customerDal);
            Result authenticatedCustomer = await customerCollection.AuthenticateCustomerAsync(model.Email,model.Password);

           



            return View("dashboard",authenticatedCustomer);
        }
    }
}
