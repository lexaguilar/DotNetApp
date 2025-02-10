﻿using System;
using System.Collections.Generic;

namespace FactApi.Domain.Models;

public partial class BillDetail
{
    public int Id { get; set; }

    public int BillId { get; set; }

    public int ProductId { get; set; }

    public decimal Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Discount { get; set; }

    public decimal Total { get; set; }

    public virtual Bill Bill { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
