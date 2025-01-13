using System;
using System.Collections.Generic;

namespace Fitness_Center.Models;

public partial class Login
{
    public string? Username { get; set; }

    public string? Password { get; set; }

    public decimal? RoleId { get; set; }

    public decimal Id { get; set; }

    public virtual Role? Role { get; set; }
}
