﻿using System;
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

        public SubsicsController(ModelContext context)
        {
            _context = context;
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
