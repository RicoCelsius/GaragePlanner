using Core;
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
        private readonly ICustomerDal _customerDal;


        public LoginController(CustomerDal customerDal)
        {
            _customerDal = customerDal;
            _customerCollection = new(_customerDal);
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
