using System;
using System.Collections.Generic;

namespace FactApi.Domain.Models;

public partial class Profession
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
