using System;
using System.Collections.Generic;

namespace Fitness_Center.Models;

public partial class Role
{
    public decimal Id { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Login> Logins { get; set; } = new List<Login>();
}
