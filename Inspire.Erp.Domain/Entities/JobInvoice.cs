using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class JobInvoice
    {
        public int? JobInvoiceId { get; set; }
        public string? JobInvoiceVoucherNo { get; set; }
        public DateTime? JobInvoiceVoucherDate { get; set; }
        public int? JobInvoiceJobId { get; set; }
        public string? JobInvoiceCustomerName { get; set; }
        public int? JobInvoiceCustomerId { get; set; }
        public int? JobInvoiceLocationId { get; set; }
        public int? JobInvoiceSalesManId { get; set; }
        public double? JobInvoiceDiscount { get; set; }
        public double? JobInvoiceVat { get; set; }
        public double? JobInvoiceVatAmt { get; set; }
        public double? JobInvoiceGrossAmount { get; set; }
        public double? JobInvoiceNetAmount { get; set; }
        public string? JobInvoiceRemarks { get; set; }
        public int? JobInvoiceUserId { get; set; }
        public int? JobInvoiceCurrencyId { get; set; }
        public int? JobInvoiceFsno { get; set; }
        public double? JobInvoiceFcRate { get; set; }
        public string? JobInvoiceLpoNo { get; set; }
        public DateTime? JobInvoiceLpoDate { get; set; }
        public string JobInvoiceSalesAccount { get; set; }
        public bool? JobInvoiceDelStatus { get; set; }
        [NotMapped]
        public List<JobInvoiceDetails> JobInvoiceDetails { get; set; }
    }
}
