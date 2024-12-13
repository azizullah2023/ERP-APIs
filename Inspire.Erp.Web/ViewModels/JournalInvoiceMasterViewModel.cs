using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class JournalInvoiceMasterViewModel
    {
        public int? JournalInvoiceMasterId { get; set; }
        public string? JournalInvoiceMasterNo { get; set; }
        public int? JournalInvoiceMasterSupplierId { get; set; }
        public DateTime? JournalInvoiceMasterDate { get; set; }
        public string JournalInvoiceMasterNarration { get; set; }
        public string JournalInvoiceMasterRefNo { get; set; }
        public int? JournalInvoiceMasterCurrencyId { get; set; }
        public bool? JournalInvoiceMasterDelStatus { get; set; }
        public List<AccountTransactionViewModel> AccountsTransactions { get; set; }
        public List<JournalInvoiceDetailsViewModel> JournalInvoiceDetails { get; set; }
    }
}
