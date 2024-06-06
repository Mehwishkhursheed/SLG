using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SLG.Models;

public partial class Sale_Order
{
    [Display(Name = "ID")]
    public int sale_id { get; set; }
    [Display(Name = "Customer")]
    public int customer_id { get; set; }
    [Display(Name = "Document Name")]
    public string name { get; set; } = null!;
    [Display(Name = "Total Amount")]
    public decimal total_amount { get; set; }
    [Display(Name = "Payment Method")]
    public int payment_method { get; set; }
    [Display(Name = "Date Created")]
    public DateTime? date_created { get; set; }
    [Display(Name = "Status")]
    public string? state { get; set; }


    public virtual ICollection<Sale_Order_Detail> Sale_Order_Details { get; set; } = new List<Sale_Order_Detail>();

    public virtual Customer customer { get; set; } = null!;
    
}