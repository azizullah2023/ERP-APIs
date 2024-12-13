using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class InvoiceSeaMaster
    {
        public int? InvoiceSeaMasterInvoiceNo { get; set; }
        public DateTime? InvoiceSeaMasterInvoiceDate { get; set; }
        public int? InvoiceSeaMasterCustomerId { get; set; }
        public int? InvoiceSeaMasterJobId { get; set; }
        public int? InvoiceSeaMasterDebitId { get; set; }
        public string InvoiceSeaMasterRemarks { get; set; }
        public bool? InvoiceSeaMasterDelStatus { get; set; }
    }
}
