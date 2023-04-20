using Core;
using DAL;
using Domain;
using GaragePlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace GaragePlanner.Controllers
{
    public class LoginController : Controller
    {
        ICustomerDal _customerDal;
        private readonly CustomerCollection _customerFile;


        public LoginController(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
            _customerFile = new CustomerCollection(_customerDal);
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
            Customer authenticatedCustomer = _customerFile.AuthenticateCustomer(model.Email,model.Password);

           



            return View("dashboard",authenticatedCustomer);
        }
    }
}
