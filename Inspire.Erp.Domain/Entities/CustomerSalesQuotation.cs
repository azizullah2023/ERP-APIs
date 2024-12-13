using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Inspire.Erp.Domain.Entities
{
    public partial class CustomerSalesQuotation
    {
        public long CustomerQuotationQid { get; set; }
        public string CustomerQuotationVoucherNo { get; set; }
        public DateTime? customerQuotationQuotationdate { get; set; }
        public long? CustomerQuotationDetailsCashCustomerName { get; set; }
        public decimal? CustomerQuotationDetailsQty { get; set; }
        public decimal? CustomerQuotationDetailsUnitPrice { get; set; }
        public long? CustomerQuotationDetailsItemId { get; set; }
        public string? CustomerQuotationDetailsDesription { get; set; }
        public decimal? CustomerQuotationDetailsAmount { get; set; }
        public long? CustomerQuotationDetailsUnits { get; set; }
        public string? CustomerQuotationDetailsRemarks { get; set; }
        public decimal? CustomerQuotationVatPercentage { get; set; }
        public decimal? CustomerQuotationDiscountPercentage { get; set; }
        public decimal? CustomerQuotationVatAmount { get; set; }
        public decimal? CustomerQuotationDiscountAmountTotal { get; set; }








    }
}
