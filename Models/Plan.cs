using System;
using System.Collections.Generic;

namespace Fitness_Center.Models;

public partial class Plan
{
    public decimal Id { get; set; }

    public string? Planname { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<Subsic> Subsics { get; set; } = new List<Subsic>();
}
