using System;
using System.Collections.Generic;

namespace Fitness_Center.Models;

public partial class Gymtext
{
    public decimal Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;
}
