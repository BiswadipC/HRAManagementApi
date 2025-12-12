using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class EmployeesAddress
{
    public int IdNo { get; set; }

    public int EmployeeMainIdNo { get; set; }

    public string PreAddress { get; set; } = null!;

    public string? PreState { get; set; }

    public string? PreDistrict { get; set; }

    public string? PreCity { get; set; }

    public string PrePin { get; set; } = null!;

    public string PreCountry { get; set; } = null!;

    public string PermAddress { get; set; } = null!;

    public string? PermState { get; set; }

    public string? PermDistrict { get; set; }

    public string? PermCity { get; set; }

    public string PermPin { get; set; } = null!;

    public string PermCountry { get; set; } = null!;

    public virtual Employee EmployeeMainIdNoNavigation { get; set; } = null!;
}
