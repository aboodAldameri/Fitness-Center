using Microsoft.AspNetCore.Mvc;

namespace Fitness_Center.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            ViewData["name"] = HttpContext.Session.GetString("adminName");
            ViewData["id"] = HttpContext.Session.GetInt32("adminId");

            return View();
        }
    }
}
