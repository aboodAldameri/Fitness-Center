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
    public class SubsicriptionsController : Controller
    {
        private readonly ModelContext _context;

        public SubsicriptionsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Subsicriptions
        public async Task<IActionResult> Index()
        {
              return _context.Subsicriptions != null ? 
                          View(await _context.Subsicriptions.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Subsicriptions'  is null.");
        }

        // GET: Subsicriptions/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Subsicriptions == null)
            {
                return NotFound();
            }

            var subsicription = await _context.Subsicriptions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subsicription == null)
            {
                return NotFound();
            }

            return View(subsicription);
        }

        // GET: Subsicriptions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subsicriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumOfMonths")] Subsicription subsicription)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subsicription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subsicription);
        }

        // GET: Subsicriptions/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Subsicriptions == null)
            {
                return NotFound();
            }

            var subsicription = await _context.Subsicriptions.FindAsync(id);
            if (subsicription == null)
            {
                return NotFound();
            }
            return View(subsicription);
        }

        // POST: Subsicriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,NumOfMonths")] Subsicription subsicription)
        {
            if (id != subsicription.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subsicription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubsicriptionExists(subsicription.Id))
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
            return View(subsicription);
        }

        // GET: Subsicriptions/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Subsicriptions == null)
            {
                return NotFound();
            }

            var subsicription = await _context.Subsicriptions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subsicription == null)
            {
                return NotFound();
            }

            return View(subsicription);
        }

        // POST: Subsicriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Subsicriptions == null)
            {
                return Problem("Entity set 'ModelContext.Subsicriptions'  is null.");
            }
            var subsicription = await _context.Subsicriptions.FindAsync(id);
            if (subsicription != null)
            {
                _context.Subsicriptions.Remove(subsicription);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubsicriptionExists(decimal id)
        {
          return (_context.Subsicriptions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
