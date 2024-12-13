using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class InvoiceAirDetails
    {
        public int? InvoiceAirDetailsSno { get; set; }
        public string InvoiceAirDetailsInvoiceNo { get; set; }
        public string InvoiceAirDetailsAccNo { get; set; }
        public double? InvoiceAirDetailsAmount { get; set; }
        public string InvoiceAirDetailsRemarks { get; set; }
        public bool? InvoiceAirDetailsDelStatus { get; set; }
    }
}
