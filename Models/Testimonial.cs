using System;
using System.Collections.Generic;

namespace Fitness_Center.Models;

public partial class Testimonial
{
    public decimal Id { get; set; }

    public string? Customername { get; set; }

    public string? Testimonial1 { get; set; }

    public bool? Status { get; set; }
}
