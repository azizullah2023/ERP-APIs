using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class InvoiceSeaDetails
    {
        public int? InvoiceSeaDetailsSno { get; set; }
        public string InvoiceSeaDetailsInvoiceNo { get; set; }
        public string InvoiceSeaDetailsAccNo { get; set; }
        public double? InvoiceSeaDetailsAmount { get; set; }
        public string InvoiceSeaDetailsRemarks { get; set; }
        public bool? InvoiceSeaDetailsDelStatus { get; set; }
    }
}
