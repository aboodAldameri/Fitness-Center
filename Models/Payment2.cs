using System;
using System.Collections.Generic;

namespace Fitness_Center.Models;

public partial class Payment2
{
    public decimal Id { get; set; }

    public decimal Subscriptionid { get; set; }

    public string? Bankname { get; set; }

    public string? Iban { get; set; }

    public decimal? Total { get; set; }

    public virtual Subsic Subscription { get; set; } = null!;
}
