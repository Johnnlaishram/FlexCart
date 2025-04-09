using System;
using System.Collections.Generic;

namespace FlexCart.Models;

public partial class Purchase
{
    public int PurCode { get; set; }

    public string? Purchaser { get; set; }

    public string? PurchaseDate { get; set; }

    public decimal? NetAmt { get; set; }

    public string? VoucherFile { get; set; }
}
