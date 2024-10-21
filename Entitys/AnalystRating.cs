using System;
using System.Collections.Generic;

namespace StockMaster.Entitys;

public partial class AnalystRating
{
    public int RatingId { get; set; }

    public int? StockId { get; set; }

    public string? AnalystName { get; set; }

    public string? Rating { get; set; }

    public DateOnly? RatingDate { get; set; }

    public virtual Stock? Stock { get; set; }
}
