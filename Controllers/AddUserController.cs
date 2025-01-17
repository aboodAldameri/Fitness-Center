using Fitness_Center.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Data;


namespace Fitness_Center.Controllers
{
    public class AddUserController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AddUserController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult AddUser()
        {
            return View();
        }

        enum EnumRole
        {
            Admin = 1,
            Employee = 2,
            Customer = 3
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser([Bind("Fname,Lname,ImageFile,UserName,Password,Role")] UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                 
                    string fileName = await SaveImageAsync(model.ImageFile);

                    if (model.Role == (int)EnumRole.Customer)
                    {
                        var customer = new Customer
                        {
                            Fname = model.Fname,
                            Lname = model.Lname,
                            ImagePath = fileName
                        };

                        _context.Add(customer);
                    }
                    else if (model.Role == (int)EnumRole.Employee)
                    {
                        var employee = new Employee
                        {
                            Fname = model.Fname,
                            Lname = model.Lname,
                            ImagePath = fileName
                        };

                        _context.Add(employee);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid user type selected.");
                        return View(model);
                    }

                    var login = new Login
                    {
                        Username = model.UserName,
                        Password = model.Password,
                        RoleId = model.Role,
                    };

                    _context.Add(login);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                }
            }

            ModelState.AddModelError("", "Invalid input data.");
            return View(model);
        }

        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            if (imageFile == null)
                throw new ArgumentException("Image file is required.");

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            string path = Path.Combine(wwwRootPath, "Images", fileName);

            string directoryPath = Path.Combine(wwwRootPath, "Images");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return fileName;
        }
    }
}
