using System;
using System.Collections.Generic;

namespace Inspire.Erp.Web.ViewModels.Procurement
{
    public partial class GeneralPurchaseOrderDetailsViewModel
    {
        public decimal GeneralPurchaseOrderDetailsPoid { get; set; }
        public int? GeneralPurchaseOrderDetailsSno { get; set; }
        public string GeneralPurchaseOrderDetailsDescription { get; set; }
        public string GeneralPurchaseOrderDetailsMaterialDescription { get; set; }
        public double? GeneralPurchaseOrderDetailsQuantity { get; set; }
        public double? GeneralPurchaseOrderDetailsRate { get; set; }
        public double? GeneralPurchaseOrderDetailsAmount { get; set; }
        public double? GeneralPurchaseOrderDetailsFcAmount { get; set; }
        public int? GeneralPurchaseOrderDetailsUnitId { get; set; }
        public int? GeneralPurchaseOrderDetailsMaterialId { get; set; }
        public int? GeneralPurchaseOrderDetailsPreqId { get; set; }
        public bool? GeneralPurchaseOrderDetailsIsEdit { get; set; }
        public string GeneralPurchaseOrderDetailsPoStatus { get; set; }
        public int? GeneralPurchaseOrderDetailsPodId { get; set; }
        public int? GeneralPurchaseOrderDetailsSrdId { get; set; }
        public bool? GeneralPurchaseOrderDetailsDelStatus { get; set; }
        public decimal GeneralPurchaseOrderId { get; set; }
        public string GeneralPurchaseOrderPono { get; set; }
    }
}
