using System;
using System.Collections.Generic;

namespace FlexCart.Models;

public partial class Return
{
    public int ReturnId { get; set; }

    public string? ReturnType { get; set; }

    public DateTime? ReturnDate { get; set; }

    public int? ProductCode { get; set; }

    public string? SalesCode { get; set; }

    public DateTime? SalesDate { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? ReturnAmt { get; set; }
}
