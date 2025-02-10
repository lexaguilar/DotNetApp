using System;
using System.Collections.Generic;

namespace FactApi.Domain.Models;

public partial class Contract
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public DateTime ContractDate { get; set; }

    public DateTime Init { get; set; }

    public DateTime EndDate { get; set; }

    public int ClientId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string PathDocument { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;
}
