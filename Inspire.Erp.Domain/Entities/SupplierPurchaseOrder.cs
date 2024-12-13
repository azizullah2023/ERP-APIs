using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class SupplierPurchaseOrder
    {
        public int? SupplierPurchaseOrderId { get; set; }
        public DateTime? SupplierPurchaseOrderDate { get; set; }
        public int? SupplierPurchaseOrderInsId { get; set; }
        public string SupplierPurchaseOrderRemarks { get; set; }
        public double? SupplierPurchaseOrderDiscPercentage { get; set; }
        public double? SupplierPurchaseOrderDiscAmt { get; set; }
        public double? SupplierPurchaseOrderTotal { get; set; }
        public double? SupplierPurchaseOrderGrandTotal { get; set; }
        public int? SupplierPurchaseOrderSpoAboutId { get; set; }
        public int? SupplierPurchaseOrderCurrencyId { get; set; }
        public string SupplierPurchaseOrderSubject { get; set; }
        public string SupplierPurchaseOrderNote { get; set; }
        public string SupplierPurchaseOrderWarranty { get; set; }
        public string SupplierPurchaseOrderTraining { get; set; }
        public string SupplierPurchaseOrderTechDetails { get; set; }
        public string SupplierPurchaseOrderTerms { get; set; }
        public DateTime? SupplierPurchaseOrderCpoDeliveryTime { get; set; }
        public string SupplierPurchaseOrderPacking { get; set; }
        public string SupplierPurchaseOrderQuality { get; set; }
        public bool? SupplierPurchaseOrderIsLocalPurchase { get; set; }
        public bool? SupplierPurchaseOrderDelStatus { get; set; }
    }
}
