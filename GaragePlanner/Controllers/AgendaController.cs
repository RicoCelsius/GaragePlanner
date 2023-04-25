using Microsoft.AspNetCore.Mvc;

namespace GaragePlanner.Controllers
{
    public class AgendaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
