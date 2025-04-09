using System;
using System.Collections.Generic;

namespace FlexCart.Models;

public partial class SalesProduct
{
    public int SalesProCode { get; set; }

    public int? SalesCode { get; set; }

    public int? ProdCode { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? Rate { get; set; }
}
