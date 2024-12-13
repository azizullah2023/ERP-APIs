using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class JobInvoiceDetails
    {
        public int? JobInvoiceDetailsDetailsId { get; set; }
        public string? JobInvoiceDetailsVoucherNo { get; set; }
        public int? JobInvoiceDetailsItemId { get; set; }
        public string? JobInvoiceDetailsDescription { get; set; }
        public int? JobInvoiceDetailsUnitId { get; set; }
        public double? JobInvoiceDetailsSoldQty { get; set; }
        public double? JobInvoiceDetailsUnitPrice { get; set; }
        public string JobInvoiceDetailsUnit { get; set; }
        public double? JobInvoiceDetailsGrossAmount { get; set; }
        public double? JobInvoiceDetailsDiscount { get; set; }
        public double? JobInvoiceDetailsNetAmount { get; set; }
        public int? JobInvoiceDetailsFsno { get; set; }
        public int? JobInvoiceDetailsSno { get; set; }
        public int? JobInvoiceDetailsLocationId { get; set; }
        public bool? JobInvoiceDetailsDelStatus { get; set; }
    }
}
