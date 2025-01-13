using Microsoft.AspNetCore.Mvc;

namespace Fitness_Center.Controllers
{
    public class TrainerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
