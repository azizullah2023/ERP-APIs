using System;
using System.Collections.Generic;

namespace Inspire.Erp.Domain.Entities
{
    public partial class SupplierPurchaseOrderDetails
    {
        public int? SupplierPurchaseOrderDetailsDetailsId { get; set; }
        public int? SupplierPurchaseOrderDetailsSpoId { get; set; }
        public int? SupplierPurchaseOrderDetailsSlno { get; set; }
        public int? SupplierPurchaseOrderDetailsItemId { get; set; }
        public int? SupplierPurchaseOrderDetailsUnitId { get; set; }
        public double? SupplierPurchaseOrderDetailsQty { get; set; }
        public double? SupplierPurchaseOrderDetailsUnitPrice { get; set; }
        public double? SupplierPurchaseOrderDetailsAmount { get; set; }
        public string SupplierPurchaseOrderDetailsDescription { get; set; }
        public bool? SupplierPurchaseOrderDetailsIsEdited { get; set; }
        public bool? SupplierPurchaseOrderDetailsDelStatus { get; set; }
    }
}
