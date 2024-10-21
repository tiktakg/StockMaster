using System;
using System.Collections.Generic;

namespace StockMaster.Entitys;

public partial class Alert
{
    public int AlertId { get; set; }

    public int? UserId { get; set; }

    public int? StockId { get; set; }

    public string? AlertType { get; set; }

    public decimal? ThresholdValue { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Stock? Stock { get; set; }

    public virtual User? User { get; set; }
}
