using System;
using System.Collections.Generic;

namespace StockMaster.Entitys;

public partial class Sectors
{
    public int SectorId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
