using System;
using System.Collections.Generic;

namespace Fitness_Center.Models;


public partial class Pagecontent
{
    public decimal Id { get; set; }

    public string Sectionname { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Subtitle { get; set; }

    public string? Description { get; set; }

    public string? Imageurl { get; set; }
}
