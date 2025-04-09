using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FlexCart.Models;

public partial class Customer
{
    [Key]
    public int CusId { get; set; }
    [Required(ErrorMessage = "Name is Required.")]
    [StringLength(100)]
    public string? Name { get; set; }= string.Empty;
    [Required(ErrorMessage = "Address is Required.")]
    [StringLength(100)]
    public string? Address { get; set; } = string.Empty;
    [Required(ErrorMessage = "Email is Required.")]
    [StringLength(100)]
    public string? Email { get; set; }=string.Empty;
    [Required(ErrorMessage = "Mobile is Required.")]
    [StringLength(10)]
    public string? Mobile { get; set; }=string.Empty ;
}
