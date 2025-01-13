using System;
using System.Collections.Generic;

namespace Fitness_Center.Models;

public partial class Subsicription
{
    public decimal Id { get; set; }

    public decimal? NumOfMonths { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
