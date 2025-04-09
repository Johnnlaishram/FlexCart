using System;
using System.Collections.Generic;

namespace FlexCart.Models;

public partial class PurchaseProduct
{
    public int PurProdCode { get; set; }

    public int? PurCode { get; set; }

    public string? BatchNo { get; set; }

    public DateTime? MfgDate { get; set; }
}
