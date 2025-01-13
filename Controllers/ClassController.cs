using Microsoft.AspNetCore.Mvc;

namespace Fitness_Center.Controllers
{
    public class ClassController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
