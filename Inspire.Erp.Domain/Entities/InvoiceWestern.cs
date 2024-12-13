using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class InvoiceWestern
    {
        public int? InvoiceWesternId { get; set; }
        public string InvoiceWesternInvNo { get; set; }
        public int? InvoiceWesternCsId { get; set; }
        public int? InvoiceWesternJobId { get; set; }
        public DateTime? InvoiceWesternInvDate { get; set; }
        public string InvoiceWesternRemarks { get; set; }
        public string InvoiceWesternLpoRefNo { get; set; }
        public string InvoiceWesternQtnNo { get; set; }
        public double? InvoiceWesternTotalAmount { get; set; }
        public string InvoiceWesternStatus { get; set; }
        public string InvoiceWesternType { get; set; }
        public double? InvoiceWesternDiscountPercentage { get; set; }
        public double? InvoiceWesternDiscount { get; set; }
        public int? InvoiceWesternUnitId { get; set; }
        public string InvoiceWesternMake { get; set; }
        public string InvoiceWesternVehNo { get; set; }
        public string InvoiceWesternChasis { get; set; }
        public string InvoiceWesternPrintInv { get; set; }
        public int? InvoiceWesternLocationId { get; set; }
        public string InvoiceWesternBank { get; set; }
        public int? InvoiceWesternCurrencyId { get; set; }
        public double? InvoiceWesternCurrencyRate { get; set; }
        public int? InvoiceWesternFsno { get; set; }
        public bool? InvoiceWesternDelStatus { get; set; }
    }
}
