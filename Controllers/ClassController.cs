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


        [HttpPost]
        public async Task<IActionResult> Purchase(decimal PlanId, int NumOfMonths, string CardNumber)
        {
            // استرجاع الخطة من قاعدة البيانات
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

         
            // حساب القيمة الإجمالية
            var totalAmount = plan.Price * NumOfMonths;

            // التحقق من وجود رصيد كافٍ
            if (paymentCard.Total < totalAmount)
            {
                ModelState.AddModelError("", "Insufficient balance on the card.");
                return RedirectToAction("Index");
            }

            // خصم المبلغ من البطاقة
            paymentCard.Total -= totalAmount;

            // حساب تواريخ الاشتراك
            var startDate = DateTime.Now;
            var endDate = startDate.AddMonths(NumOfMonths);

            // إنشاء اشتراك جديد
            var subscription = new Subsic
            {
                Planid = PlanId,
                NumOfMonths = NumOfMonths,
                Startdate = startDate,
                Enddate = endDate,
                Totalamount = totalAmount
            };

            _context.Subsics.Add(subscription);

            // تحديث رصيد البطاقة في جدول Payment
            _context.Payments.Update(paymentCard);

            // حفظ التغييرات
            await _context.SaveChangesAsync();

            // رسالة نجاح
            TempData["SuccessMessage"] = "Subscription completed successfully!";
            return RedirectToAction("Index");
        }

    }
}
