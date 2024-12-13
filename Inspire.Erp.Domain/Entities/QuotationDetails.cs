using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class QuotationDetails
    {
        public int? QuotationDetailsId { get; set; }
        public int? QuotationDetailsSno { get; set; }
        public string QuotationDetailsMaterialDesc { get; set; }
        public double? QuotationDetailsQuantity { get; set; }
        public double? QuotationDetailsRate { get; set; }
        public double? QuotationDetailsAmount { get; set; }
        public double? QuotationDetailsFcAmount { get; set; }
        public int? QuotationDetailsUnitId { get; set; }
        public int? QuotationDetailsMaterialId { get; set; }
        public bool? QuotationDetailsDelStatus { get; set; }
    }
}
