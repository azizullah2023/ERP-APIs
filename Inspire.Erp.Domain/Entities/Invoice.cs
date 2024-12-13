using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class Invoice
    {
        public int? InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceCustCode { get; set; }
        public int? InvoiceCpoId { get; set; }
        public int? InvoiceBankId { get; set; }
        public string InvoiceRemarks { get; set; }
        public string InvoicePaymentTerms { get; set; }
        public double? InvoiceDiscountPercentage { get; set; }
        public double? InvoiceDiscountAmount { get; set; }
        public bool? InvoiceDelStatus { get; set; }
    }
}
