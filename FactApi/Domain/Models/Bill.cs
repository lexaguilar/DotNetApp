using System;
using System.Collections.Generic;

namespace FactApi.Domain.Models;

public partial class Bill
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public DateTime BillDate { get; set; }

    public decimal Total { get; set; }

    public string? Observation { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public virtual ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();

    public virtual Client Client { get; set; } = null!;
}
