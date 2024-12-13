using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class SupplierQuotation
    {
        public int? SupplierQuotationId { get; set; }
        public DateTime? SupplierQuotationDate { get; set; }
        public int? SupplierQuotationSupplierId { get; set; }
        public int? SupplierQuotationEnqId { get; set; }
        public string SupplierQuotationRemarks { get; set; }
        public double? SupplierQuotationDiscPercentage { get; set; }
        public double? SupplierQuotationDiscAmt { get; set; }
        public double? SupplierQuotationGtotal { get; set; }
        public string SupplierQuotationStatus { get; set; }
        public int? SupplierQuotationJobNo { get; set; }
        public string SupplierQuotationWeight { get; set; }
        public string SupplierQuotationCbm { get; set; }
        public int? SupplierQuotationCurrencyId { get; set; }
        public int? SupplierQuotationEnqDetId { get; set; }
        public bool? SupplierQuotationDelStatus { get; set; }
    }
}
