using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Models.Procurement.PurchaseOrderTracking
{
   public class POTrackingCommon
    {
        public string PONO { get; set; }
        public DateTime? PODate { get; set; }
        public string Supplier { get; set; }
        public string Currency { get; set; }
        public decimal? CurrencyRate { get; set; }
        public decimal? Amount { get; set; }
        public decimal AmountAED { get; set; }
        public decimal? DiscountPerc { get; set; }
        public decimal? VATPerc { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string DeliveryStatus { get; set; }
        public string POStatus { get; set; }
        public string JobNo { get; set; }
        public string JobName { get; set; }
    }
}
