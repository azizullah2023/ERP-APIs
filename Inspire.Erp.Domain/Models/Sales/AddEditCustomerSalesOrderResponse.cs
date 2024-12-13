using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.Sales
{
    public class AddEditCustomerSalesOrderResponse
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
        public List<AddEditCustomerOrderDetailsResponse> CustomerOrderDetails { get; set; } 
    }
    public class AddEditCustomerOrderDetailsResponse
    {
        public int? CustomerOrderDetailsId { get; set; }
        public int? CustomerOrderDetailsSno { get; set; }
        public string CustomerOrderDetailsDescription { get; set; }
        public string CustomerOrderDetailsMaterialDescription { get; set; }
        public string CustomerOrderDetailsMaterialName { get; set; }
        public string CustomerOrderDetailsPartNo { get; set; }
        public string CustomerOrderDetailsCode { get; set; }
        public double? CustomerOrderDetailsQuantity { get; set; }
        public double? CustomerOrderDetailsRate { get; set; }
        public double? CustomerOrderDetailsAmount { get; set; }
        public double? CustomerOrderDetailsFcAmount { get; set; }
        public int? CustomerOrderDetailsUnitId { get; set; }
        public string CustomerOrderDetailsUnitName { get; set; }
        public int? CustomerOrderDetailsMaterialId { get; set; }
    }
}
