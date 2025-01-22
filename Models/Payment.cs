using System;
using System.Collections.Generic;

namespace Fitness_Center.Models;

public partial class Payment
{
    public decimal Id { get; set; }

    public string? Bankname { get; set; }

    public string? Iban { get; set; }

    public decimal? Total { get; set; }

    public string? Useremail { get; set; }
}
