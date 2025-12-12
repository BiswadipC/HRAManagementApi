using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class EmployeesFamilyDetail
{
    public int IdNo { get; set; }

    public int EmployeeMainIdNo { get; set; }

    public string MemberName { get; set; } = null!;

    public string? Relationship { get; set; }

    public string? AadhaarNo { get; set; }

    public string? PanNo { get; set; }

    public virtual Employee EmployeeMainIdNoNavigation { get; set; } = null!;
}
