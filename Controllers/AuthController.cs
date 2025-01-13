using Fitness_Center.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SQLitePCL;
using System.Data;

namespace Fitness_Center.Controllers
{
    public class AuthController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public AuthController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register([Bind("Id,Fname,Lname,ImagePath,ImageFile")] string userName,string password, Customer customer)
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

                    Login userLogin = new Login();
                    userLogin.Username = userName;
                    userLogin.Password = password;
                    userLogin.RoleId = 3;

                _context.Add(userLogin);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Invalid input data");
            return View(customer);

        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([Bind("userName , password ")] string userName,string password, Login useLogin)
            {
            var user = _context.Logins.Where(x => x.Username == useLogin.Username && x.Password == useLogin.Password).SingleOrDefault();

            if (user != null)
                {

                switch (user.RoleId)
                {
                    case 1:
                        HttpContext.Session.SetInt32("AdminId", (int)user.RoleId);
                        return RedirectToAction("Index","Admin");
                    case 2:
                        HttpContext.Session.SetInt32("TrainerId", (int)user.RoleId);
                        return RedirectToAction("Index","Trainer");
                    case 3:
                        HttpContext.Session.SetInt32("CustomerId", (int)user.RoleId);
                        return RedirectToAction("Index","Home");

                }
            }
            ModelState.AddModelError("", "UserName or Password are incorret");
            return View();
        }
    }
}
