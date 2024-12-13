using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public class ProgressiveInvoice
    {
        public int? ProgressiveInvoiceId { get; set; }
        public string ProgressiveInvoiceInvNo { get; set; }
        public int? ProgressiveInvoiceCustId { get; set; }
        public int? ProgressiveInvoiceJobId { get; set; }
        public DateTime? ProgressiveInvoiceInvDate { get; set; }
        public bool? ProgressiveInvoiceIsVariation { get; set; }
        public string ProgressiveInvoiceInvoiceType { get; set; }
        public string ProgressiveInvoiceReference { get; set; }
        public double? ProgressiveInvoiceTotAmount { get; set; }
        public string ProgressiveInvoiceStatus { get; set; }
        public double? ProgressiveInvoiceDiscPercentage { get; set; }
        public double? ProgressiveInvoiceDiscount { get; set; }
        public string ProgressiveInvoicePrintInv { get; set; }
        public int? ProgressiveInvoiceLocationId { get; set; }
        public string ProgressiveInvoicePayAppNo { get; set; }
        public string ProgressiveInvoiceDescription { get; set; }
        public string ProgressiveInvoiceProgress { get; set; }
        public double? ProgressiveInvoiceCurComplVal { get; set; }
        public double? ProgressiveInvoiceCurComplPer { get; set; }
        public double? ProgressiveInvoicePrevComplVal { get; set; }
        public double? ProgressiveInvoicePrevComplPer { get; set; }
        public double? ProgressiveInvoiceCumilativeVal { get; set; }
        public double? ProgressiveInvoiceCumilativePer { get; set; }
        public double? ProgressiveInvoiceAmountDue { get; set; }
        public double? ProgressiveInvoiceTotal { get; set; }
        public int? ProgressiveInvoiceFsno { get; set; }
        public int? ProgressiveInvoiceUserId { get; set; }
        public int? ProgressiveInvoiceDescriptionId { get; set; }
        public double? ProgressiveInvoicePlRetention { get; set; }
        public double? ProgressiveInvoicePlTotalAmount { get; set; }
        public bool? ProgressiveInvoiceDelStatus { get; set; }
        [NotMapped]
        public List<AccountsTransactions> AccountsTransactions { get; set; }

    }
}
