using Domain;
using Domain.utils;
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

        [HttpGet]
        public IActionResult Index(AdminViewModel model)
        {
            try
            {
                model.Brands = _carCollection.GetAllCurrentBrands();
                return View(model);
            }
            catch (DalException ex)
            {
                ErrorViewModel errorViewModel = new()
                {
                    ErrorMessage = "Technical issues, please try again later."
                };
                Console.WriteLine(ex);
                return View("Error", errorViewModel);
            }
            catch (Exception ex)
            {
                ErrorViewModel errorViewModel = new()
                {
                    ErrorMessage = "Something went wrong, please contact support"
                };
                Console.WriteLine(ex);
                return View("Error", errorViewModel);
            }
        }

        

        [HttpPost]
        public IActionResult AddBrand(string AddBrandName)
        {
            try
            {
                if (!_carCollection.TryAddBrand(AddBrandName))
                {
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
                return View("Confirmation", model);
            }
            catch (DalException ex)
            {
                ErrorViewModel errorViewModel = new()
                {
                    ErrorMessage = "Technical issues, please try again later."
                };
                Console.WriteLine(ex);
                return View("Error", errorViewModel);
            }
            catch (Exception ex)
            {
                ErrorViewModel errorViewModel = new()
                {
                    ErrorMessage = "Something went wrong, please contact support"
                };
                Console.WriteLine(ex);
                return View("Error", errorViewModel);
            }
        }

        [HttpPost]
        public IActionResult DeleteBrand(string DeleteBrandName)
        {
            try
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

                return View("Confirmation", model);
            }
            catch (DalException ex)
            {
                ErrorViewModel errorViewModel = new()
                {
                    ErrorMessage = "Technical issues, please try again later."
                };
                Console.WriteLine(ex);
                return View("Error", errorViewModel);
            }
            catch (Exception ex)
            {
                ErrorViewModel errorViewModel = new()
                {
                    ErrorMessage = "Something went wrong, please contact support"
                };
                Console.WriteLine(ex);
                return View("Error", errorViewModel);
            }

        }
    }
}
