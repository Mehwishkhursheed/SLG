using SLG.Models;

namespace SLG.ViewModels
{
    public class SalesViewModel
    {
        public Sale_Order SaleOrder { get; set; }
        public List<Sale_Order_Detail> SaleItems { get; set; }
    }
}
