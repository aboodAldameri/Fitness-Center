using Microsoft.AspNetCore.Mvc;

namespace Fitness_Center.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
