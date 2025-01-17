using System.ComponentModel.DataAnnotations;

namespace Fitness_Center.Models
{
    public class UserViewModel
    {
        public string Fname { get; set; }

        public string Lname { get; set; }

        public IFormFile ImageFile { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int Role { get; set; } 
    }
}
