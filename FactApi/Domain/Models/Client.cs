using System;
using System.Collections.Generic;

namespace FactApi.Domain.Models;

public partial class Client
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Birthdate { get; set; }

    public int ProfessionId { get; set; }

    public bool Active { get; set; }

    public string? Address { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string? Email { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual Profession Profession { get; set; } = null!;
}
