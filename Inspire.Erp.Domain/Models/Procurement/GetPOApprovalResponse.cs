using System;
using System.Collections.Generic;
using System.Text;

namespace Inspire.Erp.Domain.Modals.Procurement
{
 public   class GetPOApprovalResponse
    {
        public int PurchaseOrderId { get; set; }
        public string purchaseOrderNo { get; set; }
        public int? purchaseOrderSupplierId { get; set; }
        public string purchaseOrderSupplierName { get; set; }
        public DateTime? purchaseOrderDate { get; set; }
        public string? purchaseOrderDescription { get; set; }
        public string? purchaseOrderLpoNo { get; set; }
        public DateTime? purchaseOrderLpoDate { get; set; }
        public double? purchaseOrderTotalAmount { get; set; }
        public int? purchaseOrderPoStatus { get; set; }
        public double? purchaseOrderDiscountPercentage { get; set; }
        public int? purchaseOrderLoacationId { get; set; }
        public int? purchaseOrderCurrencyId { get; set; }
        public double? purchaseOrderNetAmount { get; set; }
        public int? purchaseOrderFsno { get; set; }
        public int? purchaseOrderUserId { get; set; }
        public string? purchaseOrderTermsAndCondition { get; set; }
        public int? purchaseOrderJobId { get; set; }
        public string purchaseOrderJobName { get; set; }
        public DateTime? purchaseOrderApprovedDate { get; set; }
        public string? purchaseOrderApprovedStatus { get; set; }
        public int? purchaseOrderApprovedBy { get; set; }
        public string? purchaseOrderHeader { get; set; }
        public string? purchaseOrderFooter { get; set; }
        public string? purchaseOrderTerms { get; set; }
        public string? purchaseOrderPaymentTerms { get; set; }
        public string? purchaseOrderDelivery { get; set; }
        public string? purchaseOrderIndentNo { get; set; }
        public string? purchaseOrderContactPersonV { get; set; }
        public string? purchaseOrderRfqIdN { get; set; }
        public string? purchaseOrderCustomerEnquiryIdN { get; set; }
        public string? purchaseOrderSupplierQuatationIdN { get; set; }
        public bool? purchaseOrderDelStatus { get; set; }
    }
}
