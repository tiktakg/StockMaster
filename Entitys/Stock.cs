using System;
using System.Collections.Generic;

namespace StockMaster.Entitys;

public partial class Stock
{
    public int StockId { get; set; }

    public string Symbol { get; set; } = null!;

    public int? CompanyId { get; set; }

    public int? SectorId { get; set; }

    public decimal? CurrentPrice { get; set; }

    public decimal? MarketCap { get; set; }

    public virtual ICollection<Alert> Alerts { get; set; } = new List<Alert>();

    public virtual ICollection<AnalystRating> AnalystRatings { get; set; } = new List<AnalystRating>();

    public virtual Companies? Company { get; set; }

    public virtual ICollection<Dividend> Dividends { get; set; } = new List<Dividend>();

    public virtual ICollection<PortfolioStock> PortfolioStocks { get; set; } = new List<PortfolioStock>();

    public virtual Sectors? Sector { get; set; }

    public virtual ICollection<WatchlistStock> WatchlistStocks { get; set; } = new List<WatchlistStock>();
}
