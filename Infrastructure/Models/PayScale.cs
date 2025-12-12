using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class PayScale
{
    public int IdNo { get; set; }

    public string PayScaleCode { get; set; } = null!;

    public decimal BasicSalary { get; set; }

    public decimal? HraPercentage { get; set; }

    public decimal? DaPercentage { get; set; }

    public decimal? ConveyanceAllowance { get; set; }

    public decimal? MedicalAllowance { get; set; }

    public decimal? OtherFixedAllowances { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
