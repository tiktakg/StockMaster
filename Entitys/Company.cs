using System;
using System.Collections.Generic;

namespace StockMaster.Entitys;

public partial class Company
{
    public int CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public string? Industry { get; set; }

    public string? Ceo { get; set; }

    public string? Headquarters { get; set; }

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
