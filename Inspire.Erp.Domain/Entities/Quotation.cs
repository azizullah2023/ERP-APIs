using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class Quotation
    {
        public int QuotationId { get; set; }
        public string QuotationNo { get; set; }
        public DateTime? QuotationDate { get; set; }
        public DateTime? QuotationValidTill { get; set; }
        public string QuotationCustName { get; set; }
        public int? QuotationCustId { get; set; }
        public int? QuotationSalesManId { get; set; }
        public string QuotationTermsCondition { get; set; }
        public string QuotationRemarks { get; set; }
        public double? QuotationTotalAmount { get; set; }
        public double? QuotationDiscPercentage { get; set; }
        public double? QuotationDiscount { get; set; }
        public int? QuotationLocationId { get; set; }
        public int? QuotationCurrencyId { get; set; }
        public double? QuotationNetAmount { get; set; }
        public int? QuotationFsno { get; set; }
        public int? QuotationUserId { get; set; }
        public bool? QuotationDelStatus { get; set; }
    }
}
