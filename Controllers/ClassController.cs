using Fitness_Center.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Fitness_Center.Controllers
{
    public class ClassController : Controller
    {
        private readonly ModelContext _context;

        public ClassController(ModelContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // جلب البيانات من جدول Plans
            var plans = await _context.Plans.ToListAsync();

            // تمرير البيانات إلى View
            return View(plans);
        }
    }
}
