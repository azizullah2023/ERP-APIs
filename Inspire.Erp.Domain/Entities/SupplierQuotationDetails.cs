using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class SupplierQuotationDetails
    {
        public int? SupplierQuotationDetailsId { get; set; }
        public int? SupplierQuotationDetailsDetId { get; set; }
        public int? SupplierQuotationDetailsSno { get; set; }
        public int? SupplierQuotationDetailsMaterialId { get; set; }
        public int? SupplierQuotationDetailsUnitId { get; set; }
        public double? SupplierQuotationDetailsQty { get; set; }
        public double? SupplierQuotationDetailsRate { get; set; }
        public double? SupplierQuotationDetailsAmount { get; set; }
        public string SupplierQuotationDetailsDremarks { get; set; }
        public bool? SupplierQuotationDetailsIsEdiat { get; set; }
        public double? SupplierQuotationDetailsFcAmount { get; set; }
        public int? SupplierQuotationDetailsEnqDetId { get; set; }
        public bool? SupplierQuotationDetailsDelStatus { get; set; }
    }
}
