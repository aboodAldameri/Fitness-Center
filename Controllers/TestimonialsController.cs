using Fitness_Center.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fitness_Center.Controllers
{
    public class TestimonialsController : Controller
    {
        private readonly ModelContext _context;

        public TestimonialsController(ModelContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                testimonial.Status = false; 
                _context.Testimonials.Add(testimonial);
                _context.SaveChanges();

                ViewBag.Message = "Thank you! Your testimonial has been submitted for review.";
                return RedirectToAction("Index");
            }

            return View(testimonial);
        }
    }
}
