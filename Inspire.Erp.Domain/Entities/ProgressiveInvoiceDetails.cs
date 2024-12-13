using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class ProgressiveInvoiceDetails
    {
        public int ProgressiveInvoiceDetailsPrDetId { get; set; }
        public int? ProgressiveInvoiceDetailsId { get; set; }
        public int? ProgressiveInvoiceDetailsJobDecId { get; set; }
        public int? ProgressiveInvoiceDetailsDescrId { get; set; }
        public double? ProgressiveInvoiceDetailsAmount { get; set; }
        public double? ProgressiveInvoiceDetailsPerValue { get; set; }
        public string ProgressiveInvoiceDetailsRemarks { get; set; }
        public bool? ProgressiveInvoiceDetailsIsEdit { get; set; }
        public bool? ProgressiveInvoiceDetailsDelStatus { get; set; }
    }
}
