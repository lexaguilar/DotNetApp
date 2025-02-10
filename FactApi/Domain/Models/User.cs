using System;
using System.Collections.Generic;

namespace FactApi.Domain.Models;

public partial class User
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool Active { get; set; }

    public string Email { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public int CompanyId { get; set; }

    public virtual Company Company { get; set; } = null!;
}
