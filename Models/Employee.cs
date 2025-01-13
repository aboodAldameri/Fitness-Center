using System;
using System.Collections.Generic;

namespace Fitness_Center.Models;

public partial class Employee
{
    public decimal Id { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }

    public string? ImagePath { get; set; }

    public decimal? RoleId { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual Role? Role { get; set; }
}
