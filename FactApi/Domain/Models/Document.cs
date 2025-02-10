using System;
using System.Collections.Generic;

namespace FactApi.Domain.Models;

public partial class Document
{
    public int Id { get; set; }

    public int ContractId { get; set; }

    public string PathDocument { get; set; } = null!;

    public virtual Contract Contract { get; set; } = null!;
}
