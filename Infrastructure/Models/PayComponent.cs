using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class PayComponent
{
    public int IdNo { get; set; }

    public string ComponentName { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string CalculationMethod { get; set; } = null!;

    public decimal? PercentageValue { get; set; }

    public decimal? FixedValue { get; set; }

    public string IsTaxable { get; set; } = null!;

    public string IsActive { get; set; } = null!;

    public virtual ICollection<EmployeePayComponent> EmployeePayComponents { get; set; } = new List<EmployeePayComponent>();
}
