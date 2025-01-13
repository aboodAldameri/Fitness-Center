using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fitness_Center.Models;
using Microsoft.AspNetCore.Hosting;

namespace Fitness_Center.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public CustomersController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Customers.Include(c => c.Employee).Include(c => c.Role).Include(c => c.Subscription);
            return View(await modelContext.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Employee)
                .Include(c => c.Role)
                .Include(c => c.Subscription)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id");
            ViewData["SubscriptionId"] = new SelectList(_context.Subsicriptions, "Id", "Id");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fname,Lname,ImageFile,SubscriptionId,EmployeeId,RoleId")] Customer customer)
        {
            if (ModelState.IsValid)
            {

                string wwwRootPath = _webHostEnviroment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + "_" + customer.ImageFile.FileName;
                string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await customer.ImageFile.CopyToAsync(fileStream);
                }
                customer.ImagePath = fileName;

                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", customer.EmployeeId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", customer.RoleId);
            ViewData["SubscriptionId"] = new SelectList(_context.Subsicriptions, "Id", "Id", customer.SubscriptionId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Fname,Lname,ImageFile,DateFrom,DateTo,SubscriptionId,EmployeeId,RoleId")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (customer.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + customer.ImageFile.FileName;
                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await customer.ImageFile.CopyToAsync(fileStream);
                        }
                        customer.ImagePath = fileName;
                    }

                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", customer.EmployeeId);
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", customer.RoleId);
            ViewData["SubscriptionId"] = new SelectList(_context.Subsicriptions, "Id", "Id", customer.SubscriptionId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Employee)
                .Include(c => c.Role)
                .Include(c => c.Subscription)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Customers == null)
            {
                return Problem("Entity set 'ModelContext.Customers'  is null.");
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(decimal id)
        {
          return (_context.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
