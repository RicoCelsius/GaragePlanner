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
        public IActionResult AddBrand(string AddBrandName)
        {

            if(!_carCollection.TryAddBrand(AddBrandName)){
                ErrorViewModel errorViewModel = new()
                {
                    ErrorMessage = "This brand is already existent in the system."
                };
                return View("Error", errorViewModel);
            }
            AdminViewModel model = new()
            {
                AddBrandName = AddBrandName
            };
            return View("Confirmation",model);
        }

        [HttpPost]
        public IActionResult DeleteBrand(string DeleteBrandName)
        {
            if (!_carCollection.TryDeleteBrand(DeleteBrandName))
            {
                ErrorViewModel errorViewModel = new()
                {
                    ErrorMessage = "This brand is not existent in the system."
                };
                return View("Error", errorViewModel);
            }
            AdminViewModel model = new()
            {
                DeleteBrandName = DeleteBrandName
            };

            return View("Confirmation",model);

        }
    }
}
