using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inspire.Erp.Web.ViewModels
{
    public class JournalInvoiceDetailsViewModel
    {
        public int? JournalInvoiceDetailsId { get; set; }
        public string? JournalInvoiceDetailsNo { get; set; }
        public string? JournalInvoiceDetailsAccNo { get; set; }
        public string? JournalInvoiceDetailsRefNo { get; set; }
        public double? JournalInvoiceDetailsAmount { get; set; }
        public string? JournalInvoiceDetailsRemarks { get; set; }
        public int? JournalInvoiceDetailsSlNo { get; set; }
        public int? JournalInvoiceDetailsJobId { get; set; }
        public bool? JournalInvoiceDetailsIsEdit { get; set; }
        public bool? JournalInvoiceDetailsDelStatus { get; set; }
    }
}
