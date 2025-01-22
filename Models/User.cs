using System;
using System.Collections.Generic;

namespace Fitness_Center.Models;

public partial class User
{
    public decimal Id { get; set; }

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string? ImagePath { get; set; }

    public string? Usertype { get; set; }

    public decimal? Roleid { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }
}
