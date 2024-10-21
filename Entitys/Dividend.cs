using System;
using System.Collections.Generic;

namespace StockMaster.Entitys;

public partial class Dividend
{
    public int DividendId { get; set; }

    public int? StockId { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public decimal? DividendAmount { get; set; }

    public DateOnly? AnnouncementDate { get; set; }

    public virtual Stock? Stock { get; set; }
}
