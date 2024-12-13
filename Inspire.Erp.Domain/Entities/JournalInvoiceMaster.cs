using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class JournalInvoiceMaster
    {
        public int? JournalInvoiceMasterId { get; set; }
        public string JournalInvoiceMasterNo { get; set; }
        public int? JournalInvoiceMasterSupplierId { get; set; }
        public DateTime? JournalInvoiceMasterDate { get; set; }
        public string JournalInvoiceMasterNarration { get; set; }
        public string JournalInvoiceMasterRefNo { get; set; }
        public int? JournalInvoiceMasterCurrencyId { get; set; }
        public bool? JournalInvoiceMasterDelStatus { get; set; }
    }
}
