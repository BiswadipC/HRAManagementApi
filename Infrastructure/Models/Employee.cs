using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Employee
{
    public int IdNo { get; set; }

    public string Name { get; set; } = null!;

    public decimal Salary { get; set; }

    public string Gender { get; set; } = null!;

    public int? DepartmentIdNo { get; set; }

    public int? ManagerIdNo { get; set; }

    public string? Email { get; set; }

    public DateOnly? Dob { get; set; }

    public int? DesignationIdNo { get; set; }

    public int? PayScaleIdNo { get; set; }

    public virtual Department? DepartmentIdNoNavigation { get; set; }

    public virtual Designation? DesignationIdNoNavigation { get; set; }

    public virtual ICollection<EmployeePayComponent> EmployeePayComponents { get; set; } = new List<EmployeePayComponent>();

    public virtual ICollection<EmployeesAddress> EmployeesAddresses { get; set; } = new List<EmployeesAddress>();

    public virtual ICollection<EmployeesFamilyDetail> EmployeesFamilyDetails { get; set; } = new List<EmployeesFamilyDetail>();

    public virtual PayScale? PayScaleIdNoNavigation { get; set; }
}
