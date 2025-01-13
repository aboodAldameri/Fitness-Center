using Microsoft.AspNetCore.Mvc;

namespace Fitness_Center.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
