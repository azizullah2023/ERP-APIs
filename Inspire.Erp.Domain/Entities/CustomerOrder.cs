using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class CustomerOrder
    {
        public int? CustomerOrderId { get; set; }
        public string CustomerOrderNo { get; set; }
        public int? CustomerOrderCustomerId { get; set; }
        public DateTime? CustomerOrderDate { get; set; }
        public string CustomerOrderDescription { get; set; }
        public string CustomerOrderLpoNo { get; set; }
        public DateTime? CustomerOrderLpoDate { get; set; }
        public double? CustomerOrderTotalAmount { get; set; }
        public string CustomerOrderStatus { get; set; }
        public double? CustomerOrderDiscountPercentage { get; set; }
        public double? CustomerOrderDiscount { get; set; }
        public int? CustomerOrderLocationId { get; set; }
        public int? CustomerOrderCurrencyId { get; set; }
        public double? CustomerOrderNetAmount { get; set; }
        public int? CustomerOrderFsno { get; set; }
        public int? CustomerOrderUserId { get; set; }
        public string CustomerOrderTermsAndConditions { get; set; }
        public bool? CustomerOrderDelStatus { get; set; }
    }
}
