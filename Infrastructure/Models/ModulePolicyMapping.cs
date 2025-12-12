using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class ModulePolicyMapping
{
    public int IdNo { get; set; }

    public string Username { get; set; } = null!;

    public string ModuleName { get; set; } = null!;

    public string PolicyName { get; set; } = null!;

    public string PermissionType { get; set; } = null!;
}
