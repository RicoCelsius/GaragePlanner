using Core;
using DAL;
using GaragePlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace GaragePlanner.Controllers
{
    public class LoginController : Controller
    {
        private readonly CustomerCollection _customerFile = new();

        public LoginController()
        {

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
