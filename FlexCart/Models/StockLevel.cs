using System;
using System.Collections.Generic;

namespace FlexCart.Models;

public partial class StockLevel
{
    public int StkId { get; set; }

    public decimal? AvailableStock { get; set; }

    public int? ProductCodes { get; set; }
}
