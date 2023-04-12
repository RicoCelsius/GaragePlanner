using Core;
using GaragePlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace GaragePlanner.Controllers
{
    public class LoginController : Controller
    {
        private readonly CustomerFile _customerFile = new();

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
            Credentials userCredentials = new Credentials(model.Email, model.Password);
            Customer authenticatedCustomer = _customerFile.AuthenticateCustomer(userCredentials);



            return View("dashboard",authenticatedCustomer);
        }
    }
}
