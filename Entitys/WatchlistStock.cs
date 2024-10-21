using System;
using System.Collections.Generic;

namespace StockMaster.Entitys;

public partial class WatchlistStock
{
    public int WatchlistStockId { get; set; }

    public int? StockId { get; set; }

    public DateTime? AddedAt { get; set; }

    public virtual Stock? Stock { get; set; }
}
