using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class EmployeePayComponent
{
    public int IdNo { get; set; }

    public int EmployeeId { get; set; }

    public int ComponentId { get; set; }

    public string ValueType { get; set; } = null!;

    public decimal? OverrideFixedValue { get; set; }

    public decimal? OverridePercentageValue { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual PayComponent Component { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
