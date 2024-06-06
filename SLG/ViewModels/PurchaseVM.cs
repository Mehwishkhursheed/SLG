using SLG.Models;

namespace SLG.ViewModels
{
    public class PurchaseViewModel
    {
        public Purchase_Order PurchaseOrder { get; set; }
        public List<Purchase_Order_Detail> PurchaseItems { get; set; }
    }
}
