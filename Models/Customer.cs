using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fitness_Center.Models;

public partial class Customer
{
    public decimal Id { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }

    public string? ImagePath { get; set; }
    [NotMapped]
    public virtual IFormFile ImageFile { get; set; }

    public decimal? SubscriptionId { get; set; }

    public decimal? EmployeeId { get; set; }

    public decimal? RoleId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Role? Role { get; set; }

    public virtual Subsicription? Subscription { get; set; }
}
