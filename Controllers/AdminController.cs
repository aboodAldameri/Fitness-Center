using Fitness_Center.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fitness_Center.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;

        public AdminController(ModelContext context)
        {
            _context = context;
        }

        public IActionResult ManageTestimonials()
        {
            var testimonials = _context.Testimonials.Where(t => t.Status == false).ToList();
            return View(testimonials);
        }

        [HttpPost]
        public IActionResult ApproveTestimonial(decimal id)
        {
            var testimonial = _context.Testimonials.Find(id);
            if (testimonial != null)
            {
                testimonial.Status = true;
                _context.SaveChanges();
            }
            return RedirectToAction("ManageTestimonials");
        }

        [HttpPost]
        public IActionResult RejectTestimonial(decimal id)
        {
            var testimonial = _context.Testimonials.Find(id);
            if (testimonial != null)
            {
                _context.Testimonials.Remove(testimonial);
                _context.SaveChanges();
            }
            return RedirectToAction("ManageTestimonials");
        }
        public IActionResult Index()
        {
            ViewData["name"] = HttpContext.Session.GetString("adminName");
            ViewData["id"] = HttpContext.Session.GetInt32("adminId");

            return View();
        }
    }
}
