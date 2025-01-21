using System;
using System.Collections.Generic;

namespace Fitness_Center.Models;

public partial class Subsic
{
    public decimal Id { get; set; }

    public decimal? Planid { get; set; }

    public decimal? NumOfMonths { get; set; }

    public DateTime? Startdate { get; set; }

    public DateTime? Enddate { get; set; }

    public decimal? Totalamount { get; set; }

    public virtual ICollection<Payment2> Payment2s { get; set; } = new List<Payment2>();

    public virtual Plan? Plan { get; set; }
}
