using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels.sales
{
    public partial class CustomerPurchaseOrderViewModel
    {

        public int CustomerPurchaseOrderId { get; set; }
        public string CustomerPurchaseOrderVoucherNo { get; set; }
        public DateTime? CustomerPurchaseOrderDate { get; set; }
        public long? CustomerPurchaseOrderLocationId { get; set; }
        public int? CustomerPurchaseOrderCustomerId { get; set; }
        public string CustomerPurchaseOrderCustomerName { get; set; }
        public long? CustomerPurchaseOrderSalesManId { get; set; }
        public string CustomerPurchaseOrderLpoNo { get; set; }
        public DateTime? CustomerPurchaseOrderLpoDate { get; set; }
        public long? CustomerPurchaseOrderCurrencyId { get; set; }
        public string CustomerPurchaseOrderRemarks { get; set; }
        public double? CustomerPurchaseOrderDiscountPercentage { get; set; }
        public double? CustomerPurchaseOrderDiscountAmount { get; set; }
        public double? CustomerPurchaseOrderTotal { get; set; }
        public double? CustomerPurchaseOrderVat { get; set; }
        public double? CustomerPurchaseOrderVAT_Percen { get; set; }
        public double? CustomerPurchaseOrderGrandTotal { get; set; }
        public long? CustomerPurchaseOrderFsno { get; set; }
        public long? CustomerPurchaseOrderUserId { get; set; }
        public string CustomerPurchaseOrderSubject { get; set; }
        public string CustomerPurchaseOrderNote { get; set; }
        public string CustomerPurchaseOrderWarranty { get; set; }
        public string CustomerPurchaseOrderTraining { get; set; }
        public string CustomerPurchaseOrderTechnicalDetails { get; set; }
        public string CustomerPurchaseOrderTerms { get; set; }
        public int? CustomerPurchaseOrderCpoAboutId { get; set; }

        public int? CustomerPurchaseOrderDepartmentId { get; set; }
        public DateTime? CustomerPurchaseOrderCpoDeliveryTimeDate { get; set; }
        public string CustomerPurchaseOrderPacking { get; set; }
        public string CustomerPurchaseOrderQuotationId { get; set; }
        public string CustomerPurchaseOrderQuantity { get; set; }
        public bool? CustomerPurchaseOrderIslocalPurchase { get; set; }
        public string CustomerPurchaseOrderCpoTermsV { get; set; }
        public long? CustomerPurchaseOrderJobId { get; set; }
        public int? CustomerPurchaseOrderPoEnquiryIdN { get; set; }
        public int? CustomerPurchaseOrderPoQuotationIdN { get; set; }
        public bool? CustomerPurchaseOrderDelStatus { get; set; }
        public List<AccountTransactionViewModel> AccountsTransactions { get; set; }
        public List<CustomerPurchaseOrderDetailsViewModel> CustomerPurchaseOrderDetails { get; set; }

      


    }
}
