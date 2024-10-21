using System;
using System.Collections.Generic;

namespace StockMaster.Entitys;

public partial class PortfolioStock
{
    public int PortfolioStockId { get; set; }

    public int? PortfolioId { get; set; }

    public int? StockId { get; set; }

    public int Quantity { get; set; }

    public decimal? PurchasePrice { get; set; }

    public DateOnly? PurchaseDate { get; set; }

    public virtual Portfolio? Portfolio { get; set; }

    public virtual Stock? Stock { get; set; }
}
