using Domain;
using GaragePlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace GaragePlanner.Controllers
{
    public class AdminController : Controller
    {
        private readonly CarCollection _carCollection;
        public AdminController(CarCollection collection)
        {
            _carCollection = collection;
        }

        public IActionResult Index(AdminViewModel model)
        {
            model.Brands = _carCollection.GetAllCurrentBrands();
            return View(model);
        }


        [HttpPost]
        public IActionResult AddBrand()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteBrand()
        {
            return View();

        }
    }
}
