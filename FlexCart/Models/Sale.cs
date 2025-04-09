using System;
using System.Collections.Generic;

namespace FlexCart.Models;

public partial class Sale
{
    public int SalesCode { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? SalesDate { get; set; }

    public decimal? NetAmt { get; set; }

    public decimal? PaidAmt { get; set; }

    public string? UserName { get; set; }
}
