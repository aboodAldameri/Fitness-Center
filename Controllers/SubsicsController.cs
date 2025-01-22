using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fitness_Center.Models;

namespace Fitness_Center.Controllers
{
    public class SubsicsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public SubsicsController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult SubscriptionReport()
        {
            var currentYear = DateTime.Now.Year;

            var subscriptionsPerMonth = _context.Subsics
                .Where(s => s.Startdate.HasValue && s.Startdate.Value.Year == currentYear) 
                .GroupBy(s => s.Startdate.Value.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Count = g.Count()
                })
                .ToList();

            var allMonths = Enumerable.Range(1, 12).Select(m => new
            {
                Month = m,
                Count = 0 
            }).ToList();

            var chartData = allMonths
                .GroupJoin(
                    subscriptionsPerMonth,
                    all => all.Month,
                    sub => sub.Month,
                    (all, subs) => new
                    {
                        Month = $"{all.Month}/{currentYear}",
                        Count = subs.Sum(s => s.Count)
                    })
                .OrderBy(x => x.Month)
                .ToList();

            ViewBag.ChartData = chartData;
            return View();
        }

        public async Task<IActionResult> Show()
        {
            var subsics = await _context.Subsics
                .Include(s => s.Plan) 
                .ToListAsync();

            return View(subsics); 
        }
        public IActionResult Index1(DateTime? StartDate, DateTime? EndDate)
        {
            var subscriptions = _context.Subsics.AsQueryable();

            if (StartDate.HasValue)
            {
                subscriptions = subscriptions.Where(s => s.Startdate >= StartDate.Value);
            }
            if (EndDate.HasValue)
            {
                subscriptions = subscriptions.Where(s => s.Enddate <= EndDate.Value);
            }

            ViewData["StartDate"] = StartDate?.ToString("yyyy-MM-dd");
            ViewData["EndDate"] = EndDate?.ToString("yyyy-MM-dd");

            return View(subscriptions.ToList());
        }

        


        // GET: Subsics
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Subsics.Include(s => s.Plan);
            return View(await modelContext.ToListAsync());
        }

        // GET: Subsics/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Subsics == null)
            {
                return NotFound();
            }

            var subsic = await _context.Subsics
                .Include(s => s.Plan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subsic == null)
            {
                return NotFound();
            }

            return View(subsic);
        }

        // GET: Subsics/Create
        public IActionResult Create()
        {
            ViewData["Planid"] = new SelectList(_context.Plans, "Id", "Id");
            return View();
        }

        // POST: Subsics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Planid,NumOfMonths,Startdate,Enddate,Totalamount")] Subsic subsic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subsic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Planid"] = new SelectList(_context.Plans, "Id", "Id", subsic.Planid);
            return View(subsic);
        }

        // GET: Subsics/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Subsics == null)
            {
                return NotFound();
            }

            var subsic = await _context.Subsics.FindAsync(id);
            if (subsic == null)
            {
                return NotFound();
            }
            ViewData["Planid"] = new SelectList(_context.Plans, "Id", "Id", subsic.Planid);
            return View(subsic);
        }

        // POST: Subsics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Planid,NumOfMonths,Startdate,Enddate,Totalamount")] Subsic subsic)
        {
            if (id != subsic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subsic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubsicExists(subsic.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Planid"] = new SelectList(_context.Plans, "Id", "Id", subsic.Planid);
            return View(subsic);
        }

        // GET: Subsics/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Subsics == null)
            {
                return NotFound();
            }

            var subsic = await _context.Subsics
                .Include(s => s.Plan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subsic == null)
            {
                return NotFound();
            }

            return View(subsic);
        }

        // POST: Subsics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Subsics == null)
            {
                return Problem("Entity set 'ModelContext.Subsics'  is null.");
            }
            var subsic = await _context.Subsics.FindAsync(id);
            if (subsic != null)
            {
                _context.Subsics.Remove(subsic);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubsicExists(decimal id)
        {
          return (_context.Subsics?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
