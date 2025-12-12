using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class User
{
    public int IdNo { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? RefreshToken { get; set; }

    public string IsAdmin { get; set; } = null!;
}
