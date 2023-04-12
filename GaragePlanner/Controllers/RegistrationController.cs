using Core;
using GaragePlanner.Models;
using Microsoft.AspNetCore.Mvc;




namespace GaragePlanner.Controllers
    
{
    public class RegistrationController : Controller
    {
        private readonly CustomerFile _customerFile;

        public RegistrationController()
        {
            _customerFile = new CustomerFile();
        }   

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterCustomer(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            try
            {
                _customerFile.AddCustomer(new Customer(model.FirstName,model.LastName,model.Address,model.Email,model.Password));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
