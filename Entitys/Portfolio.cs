using System;
using System.Collections.Generic;

namespace StockMaster.Entitys;

public partial class Portfolio
{
    public int PortfolioId { get; set; }

    public int? UserId { get; set; }

    public string PortfolioName { get; set; } = null!;

    public decimal? TotalValue { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<PortfolioStock> PortfolioStocks { get; set; } = new List<PortfolioStock>();

    public virtual User? User { get; set; }
}
