using SLG.Models;

namespace SLG.ViewModels
{
    public class TransferViewModel
    {
        public Transfer Transfer { get; set; }
        public List<Transfer_Detail> LineItems { get; set; }
    }
}
