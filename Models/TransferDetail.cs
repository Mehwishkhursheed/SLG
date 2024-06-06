using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SLG.Models;

public partial class Transfer_Detail
{
    public int id { get; set; }
    [Required]
    public int? transfer_id { get; set; }
    [Required]
    public int? product_id { get; set; }
    [Required]
    [Range(0, int.MaxValue)]
    public int? demand { get; set; }
    [Range(0, int.MaxValue)]
    public int? done { get; set; }

    public virtual Product? product { get; set; }

    public virtual Transfer? transfer { get; set; }
}