using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class SuppQuotationExpenseDetails
    {
        public int? SuppQuotationExpenseDetailsDetId { get; set; }
        public int? SuppQuotationExpenseDetailsSqtnid { get; set; }
        public int? SuppQuotationExpenseDetailsSno { get; set; }
        public int? SuppQuotationExpenseDetailsExpId { get; set; }
        public double? SuppQuotationExpenseDetailsExpAmount { get; set; }
        public double? SuppQuotationExpenseDetailsExpPercentage { get; set; }
        public double? SuppQuotationExpenseDetailsExpFcAmount { get; set; }
        public string SuppQuotationExpenseDetailsExpRemarks { get; set; }
        public bool? SuppQuotationExpenseDetailsIsEdit { get; set; }
        public bool? SuppQuotationExpenseDetailsDelStatus { get; set; }
    }
}
