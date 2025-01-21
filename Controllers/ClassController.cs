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
            var plans = await _context.Plans.ToListAsync();

            return View(plans);
        }


        [HttpPost]
        public async Task<IActionResult> Purchase(decimal PlanId, int NumOfMonths, string CardNumber)
        {
            var plan = await _context.Plans.FindAsync(PlanId);
            if (plan == null)
            {
                return NotFound("Plan not found");
            }

            var paymentCard = _context.Payments.FirstOrDefault(c => c.Iban == CardNumber);
            if (paymentCard == null)
            {
                ModelState.AddModelError("", "Card not found. Please provide a valid card.");
                return RedirectToAction("Index");
            }

         
            var totalAmount = plan.Price * NumOfMonths;

            if (paymentCard.Total < totalAmount)
            {
                ModelState.AddModelError("", "Insufficient balance on the card.");
                return RedirectToAction("Index");
            }

            paymentCard.Total -= totalAmount;

            var startDate = DateTime.Now;
            var endDate = startDate.AddMonths(NumOfMonths);

            var subscription = new Subsic
            {
                Planid = PlanId,
                NumOfMonths = NumOfMonths,
                Startdate = startDate,
                Enddate = endDate,
                Totalamount = totalAmount
            };

            _context.Subsics.Add(subscription);

            _context.Payments.Update(paymentCard);

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Subscription completed successfully!";
            return RedirectToAction("Index");
        }

    }
}
