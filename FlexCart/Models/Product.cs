using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlexCart.Models;

public partial class Product
{
    public int ProdCode { get; set; }

    public string? ProdName { get; set; }

    public string? Barcode { get; set; }

    public string? ProdPhoto { get; set; }

    public int? ProdTypeId { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
}
