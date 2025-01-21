using System.Linq;
using System.Diagnostics;
using Fitness_Center.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fitness_Center.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;

        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var testimonials = _context.Testimonials.Where(t => t.Status == true).ToList();

            ViewBag.Testimonials = testimonials;

            return View();
        }

        public IActionResult Index1()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
