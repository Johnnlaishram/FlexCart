using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FlexCart.Models;

[Table("company")]
public partial class Company
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CompanyId { get; set; }

    public string? CompanyName { get; set; }

    public string? Mobile { get; set; }

    public string? Addr { get; set; }

    public string? Email { get; set; }

    public string? Web { get; set; }
    public string? Password { get; set; }
}
