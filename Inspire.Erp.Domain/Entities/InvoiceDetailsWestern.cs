using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class InvoiceDetailsWestern
    {
        public int InvoiceDetailsWesternDetailsId { get; set; }
        public string InvoiceDetailsWesternInvNo { get; set; }
        public int? InvoiceDetailsWesternSno { get; set; }
        public string InvoiceDetailsWesternDescription { get; set; }
        public string InvoiceDetailsWesternAccNo { get; set; }
        public string InvoiceDetailsWesternJsize { get; set; }
        public double? InvoiceDetailsWesternQty { get; set; }
        public double? InvoiceDetailsWesternUnitPrice { get; set; }
        public double? InvoiceDetailsWesternAmount { get; set; }
        public int? InvoiceDetailsWesternJobId { get; set; }
        public int? InvoiceDetailsWesternUnitId { get; set; }
        public int? InvoiceDetailsWesternSupId { get; set; }
        public int? InvoiceDetailsWesternMatId { get; set; }
        public int? InvoiceDetailsWesternFsno { get; set; }
        public bool? InvoiceDetailsWesternIsEdit { get; set; }
        public bool? InvoiceDetailsWesternDelStatus { get; set; }
    }
}
