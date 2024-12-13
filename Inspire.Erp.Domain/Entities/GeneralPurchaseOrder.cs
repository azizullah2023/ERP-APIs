using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspire.Erp.Domain.Entities
{
    public partial class GeneralPurchaseOrder
    {

        public GeneralPurchaseOrder()
        {
            GeneralPurchaseOrderDetails = new HashSet<GeneralPurchaseOrderDetails>();

        }
        public decimal GeneralPurchaseOrderId { get; set; }
        public string GeneralPurchaseOrderPono { get; set; }
        public int? GeneralPurchaseOrderSupplierId { get; set; }
        public DateTime GeneralPurchaseOrderPoDate { get; set; }
        public string GeneralPurchaseOrderDescription { get; set; }
        public string GeneralPurchaseOrderLpoNo { get; set; }
        public DateTime? GeneralPurchaseOrderLpoDate { get; set; }
        public double? GeneralPurchaseOrderTotalAmount { get; set; }
        public string GeneralPurchaseOrderStatus { get; set; }
        public double? GeneralPurchaseOrderDiscountPercentage { get; set; }
        public double? GeneralPurchaseOrderDiscount { get; set; }
        public int? GeneralPurchaseOrderLocationId { get; set; }
        public int? GeneralPurchaseOrderCurrencyId { get; set; }
        public double? GeneralPurchaseOrderNetAmount { get; set; }
        public int? GeneralPurchaseOrderFsno { get; set; }
        public int? GeneralPurchaseOrderUserId { get; set; }
        public string GeneralPurchaseOrderTermAndCondition { get; set; }
        public int? GeneralPurchaseOrderJobId { get; set; }
        public DateTime? GeneralPurchaseOrderApproveDate { get; set; }
        public string GeneralPurchaseOrderApproveStatus { get; set; }
        public int? GeneralPurchaseOrderApprovedBy { get; set; }
        public string GeneralPurchaseOrderHeader { get; set; }
        public string GeneralPurchaseOrderFooter { get; set; }
        public string GeneralPurchaseOrderTerms { get; set; }
        public string GeneralPurchaseOrderPaymentTerms { get; set; }
        public string GeneralPurchaseOrderDelivery { get; set; }
        public string GeneralPurchaseOrderIndentNo { get; set; }
        public bool? GeneralPurchaseOrderDelStatus { get; set; }
        public virtual ICollection<GeneralPurchaseOrderDetails> GeneralPurchaseOrderDetails { get; set; }
    }
}
