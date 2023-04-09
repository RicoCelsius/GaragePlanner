using Core;
using Core.Interfaces;
using GaragePlanner.Models;
using Microsoft.AspNetCore.Mvc;




namespace GaragePlanner.Controllers
    
{
    public class RegistrationController : Controller
    {
        private readonly ICustomerService _customerService;

        public RegistrationController(ICustomerService customerService)
        {
            _customerService = customerService;
        }   

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterCustomer(CustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            try
            {
                _customerService.AddCustomer(new string[] { model.FirstName, model.LastName, model.Address, model.Email, model.Password });
            }
            catch (Exception e)
            {
                return View("Index", model);
            }

            return RedirectToAction("RegistrationSuccess");
        }

        public IActionResult RegistrationSuccess()
        {
            return View();
        }
    }

}
